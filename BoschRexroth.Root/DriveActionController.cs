using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;

namespace BoschRexroth.Root
{
    class DriveActionController : IFrameProcessor
    {
        private DriveCalibrationData DCD;
        private UserAction action;
        private int? actionAngle;
        private int? actionSpeed;

        float? prevAngle;
        float? goalDelta;

        DateTime? kickTime;
        public DriveActionController(DriveCalibrationData DCD, UserAction action, int? angle, int? speed)
        {
            this.DCD = DCD;
            this.action = action;
            this.actionAngle = angle;
            this.actionSpeed = speed;
        }

        public enum UserAction
        {
            Turn,
            Base,
            Stop,
            Pause,
        }

        static public DriveActionController StartAction(DriveCalibrationData DCD, UserAction action, int? angle, int? speed)
        {
            return new DriveActionController(DCD, action, angle, speed);
        }
        public int?[] AddFrame(DateTime ts, Mat frame)
        {
            float angle = DCD.MCD.GetMarkerAngle(frame);

            switch (action)
            {
                case UserAction.Base:
                    goalDelta = goalDelta ?? (angle < 180 ? -angle : 360 - angle);
                    break;

                case UserAction.Turn:
                    goalDelta = goalDelta ?? actionAngle.Value;
                    break;

                case UserAction.Stop:
                    goalDelta = 0;
                    prevAngle = angle;
                    return new int?[1] { 0 };

                case UserAction.Pause:
                    return new int?[0]; // ignored
            }

            if (prevAngle.HasValue)
                if (goalDelta.Value >= 0)
                    goalDelta = goalDelta.Value - MkPositive(angle - prevAngle.Value);
                else
                    goalDelta = goalDelta.Value + MkPositive(angle - prevAngle.Value);

            bool isMoving = prevAngle.HasValue && Math.Abs(angle - prevAngle.Value) > 1e-3;

            prevAngle = angle;

            if (Math.Abs(goalDelta.Value) > 45)
            {
                return new int?[1] { goalDelta.Value >= 0 ? 1 : -1 };
            }
            else
            {
                if (isMoving || kickTime.HasValue && (ts - kickTime.Value) >= TimeSpan.FromSeconds(0.8))
                {
                    kickTime = null;
                    return new int?[1] { 0 }; // Stop if was kicked or moving
                }
                else
                {
                    if (Math.Abs(goalDelta.Value) < 5)
                        return new int?[1] { null }; // Done
                    else
                    {
                        // Kick if was stopped and goal delta > 5
                        kickTime = ts;
                        return new int?[1] { goalDelta.Value >= 0 ? 1 : -1 };
                    }
                }
            }

        }

        private float MkPositive(float v)
        {
            while (v < 0)
                v += 360;
            return v;
        }

        public void Stop()
        {

        }
    }
}
