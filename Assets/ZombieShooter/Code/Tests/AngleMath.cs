namespace ZombieShooter
{
    public static class AngleMath
    {
        public static float Normalize(float angle)
        {
            angle %= 360;
            //if (angle < 0) angle += 360;
            return angle;
        }

        public static float Sub(float a, float b)
        {
            var angle = a - b;
            if (angle > 180) angle -= 360;
            if (angle <-180) angle += 360;
            return angle;
        }
    }
}