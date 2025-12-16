// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("RoUjo6PloTanrj7UgG6WzfvutSNmorZMDf71xoHbsEik6pfFrSdwDf4kHVe63eaqSrE97ndVEXdxta1I77PwxSuQflpyl1P1aZMV3fqU6RzWYE6OPxq63ox89JyD66sYjYj+vlI241x0YIvmg+Aaa5G74ti2eA5GwB41HCXyhAnkyAtAGf3FHk5uXAqIOwb8N73K/5M51oxGy2xADU+ArrHIJd0w/i0VQuGmjlqqD5DGykw1fOIxb5jOFpLYFPlOccZZmasHn/VcM5tDx7nusZDss2hkj1MYC8M6VAm7OBsJND8wE79xv840ODg4PDk6okWcwT4/gkCgptrb2FgpY2m7AC27ODY5Cbs4Mzu7ODg58Fq7ovODrJSJ2aHNG4P0MDs6ODk4");
        private static int[] order = new int[] { 7,2,6,5,9,11,10,9,8,11,13,11,12,13,14 };
        private static int key = 57;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
