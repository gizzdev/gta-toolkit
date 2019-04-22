using System;

namespace RageLib.GTA5.Helpers
{
    public class RandomGaussRow
    {
        public bool GetA(int idx)
        {
            int num = idx / 64;
            int num2 = idx % 64;
            return (this.A[num] >> num2 & 1) > 0;
        }

        public void SetA(int idx)
        {
            int num = idx / 64;
            int num2 = idx % 64;
            this.A[num] |= 1u << num2;
        }

        public bool GetB()
        {
            return this.B;
        }

        public void SetB()
        {
            this.B = true;
        }

        public static RandomGaussRow operator ^(RandomGaussRow r1, RandomGaussRow r2)
        {
            RandomGaussRow randomGaussRow = new RandomGaussRow();
            for (int i = 0; i < 16; i++)
            {
                randomGaussRow.A[i] = (r1.A[i] ^ r2.A[i]);
            }
            randomGaussRow.B = (r1.B ^ r2.B);
            return randomGaussRow;
        }

        private ulong[] A = new ulong[16];

        public bool B;
    }
}
