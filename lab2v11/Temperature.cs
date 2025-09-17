namespace lab2v11
{
    public class Temperature
    {
        private double celsius;

        public double Celsius
        {
            get { return celsius; }
            set { celsius = value; }
        }

        public double Fahrenheit
        {
            get { return celsius * 9 / 5 + 32; }
        }

        private double[] history = new double[10];

        public double this[int index]
        {
            get { return history[index]; }
            set { history[index] = value; }
        }

        public static bool operator >(Temperature t1, Temperature t2)
        {
            return t1.celsius > t2.celsius;
        }

        public static bool operator <(Temperature t1, Temperature t2)
        {
            return t1.celsius < t2.celsius;
        }

        public static bool operator ==(Temperature t1, Temperature t2)
        {
            return t1.celsius == t2.celsius;
        }

        public static bool operator !=(Temperature t1, Temperature t2)
        {
            return t1.celsius != t2.celsius;
        }

        public override bool Equals(object obj)
        {
            if (obj is Temperature t)
                return this == t;
            return false;
        }

        public override int GetHashCode()
        {
            return celsius.GetHashCode();
        }
    }
}