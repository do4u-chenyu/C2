﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace C2.Business.WebsiteFeatureDetection
{
    public static class TOTP
    {
        private static readonly byte[] mapping = { 26, 27, 28, 29, 30, 255, 255, 255, 255, 255,
                                        255, 255, 255, 255, 255, 0, 1, 2, 3, 4,
                                        5, 6, 7, 8, 9, 10, 11, 12, 13, 14,
                                        15, 16, 17, 18, 19, 20, 21, 22, 23, 24,
                                        25 };
        private const int InByteSize = 8;
        private const int OutByteSize = 5;
        private const string Base32Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567";

        public static string GetHotp(string base32EncodedSecret, long counter)
        {
            byte[] message = BitConverter.GetBytes(counter)
                .Reverse().ToArray(); // Assuming Intel machine (little endian)
            byte[] secret = base32EncodedSecret.ToByteArray();

            byte[] hash;
            using (HMACSHA1 hmac = new HMACSHA1(secret, true))
            {
                hash = hmac.ComputeHash(message);
            }
            int offset = hash[hash.Length - 1] & 0xf;
            int truncatedHash = ((hash[offset] & 0x7f) << 24) |
                ((hash[offset + 1] & 0xff) << 16) |
                ((hash[offset + 2] & 0xff) << 8) |
                (hash[offset + 3] & 0xff);
            int hotp = truncatedHash % 1000000; // 6-digit code and hence 10 power 6, that is a million
            return hotp.ToString("D6");
        }

        public static string GetTotp(string user)
        {
            string base32EncodedSecret = ToBase32String(Encoding.UTF8.GetBytes(user.ToUpper() + "qazwer!@$#")).Replace("=", "");
            DateTime epochStart = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            long counter = (long)Math.Floor((DateTime.UtcNow - epochStart).TotalSeconds / 60);
            return GetHotp(base32EncodedSecret, counter);
        }

        public static byte[] ToByteArray(this string secret)
        {
            secret = secret.ToUpperInvariant();
            byte[] byteArray = new byte[(secret.Length + 7) / 8 * 5];

            long shiftingNum = 0L;
            int srcCounter = 0;
            int destCounter = 0;
            for (int i = 0; i < secret.Length; i++)
            {
                long num = (long)mapping[secret[i] - 50];
                shiftingNum |= num << (35 - srcCounter * 5);

                if (srcCounter == 7 || i == secret.Length - 1)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        byteArray[destCounter++] = (byte)((shiftingNum >> (32 - j * 8)) & 0xff);
                    }
                    shiftingNum = 0L;
                }
                srcCounter = (srcCounter + 1) % 8;
            }

            return byteArray;
        }

        static string ToBase32String(byte[] bytes)
        {
            StringBuilder builder = new StringBuilder(bytes.Length * InByteSize / OutByteSize);

            int bytesPosition = 0;
            int bytesSubPosition = 0;
            byte outputBase32Byte = 0;
            int outputBase32BytePosition = 0;

            while (bytesPosition < bytes.Length)
            {
                int bitsAvailableInByte = Math.Min(InByteSize - bytesSubPosition, OutByteSize - outputBase32BytePosition);
                outputBase32Byte <<= bitsAvailableInByte;
                outputBase32Byte |= (byte)(bytes[bytesPosition] >> (InByteSize - (bytesSubPosition + bitsAvailableInByte)));
                bytesSubPosition += bitsAvailableInByte;
                if (bytesSubPosition >= InByteSize)
                {
                    bytesPosition++;
                    bytesSubPosition = 0;
                }

                outputBase32BytePosition += bitsAvailableInByte;
                if (outputBase32BytePosition >= OutByteSize)
                {
                    outputBase32Byte &= 0x1F;  // 0x1F = 00011111 in binary
                    builder.Append(Base32Alphabet[outputBase32Byte]);
                    outputBase32BytePosition = 0;
                }
            }

            if (outputBase32BytePosition > 0)
            {
                outputBase32Byte <<= (OutByteSize - outputBase32BytePosition);
                outputBase32Byte &= 0x1F;  // 0x1F = 00011111 in binary
                builder.Append(Base32Alphabet[outputBase32Byte]);
            }

            return builder.ToString();
        }
    }
}
