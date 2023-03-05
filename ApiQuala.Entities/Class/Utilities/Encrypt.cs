namespace ApiQuala.Entities.Class.Utilities
{
    public class Encrypt
    {
        public string B64_Encode(string str)
        {
            byte[] encbuff = System.Text.Encoding.UTF8.GetBytes(str);
            return Convert.ToBase64String(encbuff);
        }
    }
}
