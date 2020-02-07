using System;
using System.Numerics;

namespace WinForms3D {

    // Must replace with a Quaternion

    public struct Rotation3D {
        const float PI = (float)Math.PI;

        public Rotation3D(float x, float y, float z) {
            this.XPitch = x;
            this.YYaw = y;
            this.ZRoll = z;
        }

        public Vector3 ToVector() => Vector3.Transform(Vector3.UnitZ, Quaternion.CreateFromYawPitchRoll(YYaw, XPitch, ZRoll));

        public float XPitch { get; }
        public float YYaw { get; }
        public float ZRoll { get; }

        public Rotation3D ToRad() => this * MathUtils.PI_Deg;

        public static bool operator ==(Rotation3D p, Rotation3D other) => other.XPitch == p.XPitch && other.YYaw == p.YYaw && other.ZRoll == p.ZRoll;

        public static bool operator !=(Rotation3D p, Rotation3D other) => !(other == p);

        public static Rotation3D operator +(Rotation3D p, Rotation3D other) => new Rotation3D(other.XPitch + p.XPitch, other.YYaw + p.YYaw, other.ZRoll + p.ZRoll);

        public static Rotation3D operator *(Rotation3D p, float a) => new Rotation3D(p.XPitch * a, p.YYaw * a, p.ZRoll * a);

        public override bool Equals(object obj) => this.GetHashCode() == obj.GetHashCode();

        public override int GetHashCode() => new { XPitch, YYaw, ZRoll }.GetHashCode();

        public override string ToString() {
            return $"<{XPitch}\t{YYaw}\t{ZRoll}>";
        }
    }
}
