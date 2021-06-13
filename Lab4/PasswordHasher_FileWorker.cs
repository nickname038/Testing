using System;
using Xunit;
using IIG.PasswordHashingUtils;
using IIG.FileWorker;


namespace Lab4
{
    public class PasswordHasher_FileWorker
    {

        [Theory]

        [InlineData("", "пароль", 0, "file1.txt")]
        [InlineData("aaaaaa  ?::*:%:aaaa", "😀本姐根", 1, "file2.txt")]
        [InlineData("1234567", null, 16, "file3.txt")]
        [InlineData("пароль", "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", 16, "file4.txt")]
        [InlineData("😀本姐根", "涼肖啖肖", 16, "file5.txt")]
        [InlineData("pswd", "1234567", 345, "file6.txt")]
        [InlineData(null, "passwd", 16, "file7.txt")]
        [InlineData("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", "", 16, "file8.txt")]

        public void Write_ReadAll_PasswordHasher_Worker(string password, string sult, uint adlerMod32, string path)
        {
            var res1 = PasswordHasher.GetHash(password, sult, adlerMod32);
            Assert.True(BaseFileWorker.Write(res1, path));
            var res2 = BaseFileWorker.ReadAll(path);
            Assert.Equal(res1, res2);
        }

        [Theory]

        [InlineData("", "пароль", 0, "file1.txt")]
        [InlineData("aaaaaa  ?::*:%:aaaa", "😀本姐根", 1, "file2.txt")]
        [InlineData("1234567", null, 16, "file3.txt")]
        [InlineData("пароль", "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", 16, "file4.txt")]
        [InlineData("😀本姐根", "涼肖啖肖", 16, "file5.txt")]
        [InlineData("pswd", "1234567", 345, "file6.txt")]
        [InlineData(null, "passwd", 16, "file7.txt")]
        [InlineData("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", "", 16, "file8.txt")]

        public void TryWrite_ReadAll_PasswordHasher_Worker(string password, string sult, uint adlerMod32, string path)
        {
            var res1 = PasswordHasher.GetHash(password, sult, adlerMod32);
            Assert.True(BaseFileWorker.TryWrite(res1, path));
            var res2 = BaseFileWorker.ReadAll(path);
            Assert.Equal(res1, res2);
        }

        [Theory]

        [InlineData("", "пароль", 0, "file1.txt")]
        [InlineData("aaaaaa  ?::*:%:aaaa", "😀本姐根", 1, "file2.txt")]
        [InlineData("1234567", null, 16, "file3.txt")]
        [InlineData("пароль", "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", 16, "file4.txt")]
        [InlineData("😀本姐根", "涼肖啖肖", 16, "file5.txt")]
        [InlineData("pswd", "1234567", 345, "file6.txt")]
        [InlineData(null, "passwd", 16, "file7.txt")]
        [InlineData("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", "", 16, "file8.txt")]

        public void TryWrite_ReadLines_PasswordHasher_Worker(string password, string sult, uint adlerMod32, string path)
        {
            var res1 = PasswordHasher.GetHash(password, sult, adlerMod32);
            Assert.True(BaseFileWorker.TryWrite(res1, path));
            var res2 = BaseFileWorker.ReadLines(path);
            Assert.True(res2.Length == 1);
            Assert.Equal(res1, res2[0]);
        }

        [Theory]

        [InlineData("", "пароль", 0, "file1.txt")]
        [InlineData("aaaaaa  ?::*:%:aaaa", "😀本姐根", 1, "file2.txt")]
        [InlineData("1234567", null, 16, "file3.txt")]
        [InlineData("пароль", "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", 16, "file4.txt")]
        [InlineData("😀本姐根", "涼肖啖肖", 16, "file5.txt")]
        [InlineData("pswd", "1234567", 345, "file6.txt")]
        [InlineData(null, "passwd", 16, "file7.txt")]
        [InlineData("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", "", 16, "file8.txt")]

        public void Write_ReadLines_PasswordHasher_Worker(string password, string sult, uint adlerMod32, string path)
        {
            var res1 = PasswordHasher.GetHash(password, sult, adlerMod32);
            Assert.True(BaseFileWorker.Write(res1, path));
            var res2 = BaseFileWorker.ReadLines(path);
            Assert.True(res2.Length == 1);
            Assert.Equal(res1, res2[0]);
        }

        [Theory]

        [InlineData("", "пароль", 0, "file1.txt", "file11.txt")]
        [InlineData("aaaaaa  ?::*:%:aaaa", "😀本姐根", 1, "file2.txt", "file22.txt")]
        [InlineData("1234567", null, 16, "file3.txt", "file33.txt")]
        [InlineData("пароль", "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", 16, "file4.txt", "file44.txt")]
        [InlineData("😀本姐根", "涼肖啖肖", 16, "file5.txt", "file55.txt")]
        [InlineData("pswd", "1234567", 345, "file6.txt", "file66.txt")]
        [InlineData(null, "passwd", 16, "file7.txt", "file77.txt")]
        [InlineData("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", "", 16, "file8.txt", "file88.txt")]

        public void Write_TryCopy_ReadAll_PasswordHasher_Worker(string password, string sult, uint adlerMod32, string path1, string path2)
        {
            var res1 = PasswordHasher.GetHash(password, sult, adlerMod32);
            Assert.True(BaseFileWorker.Write(res1, path1));
            Assert.True(BaseFileWorker.TryCopy(path1, path2, true));
            var res2 = BaseFileWorker.ReadAll(path2);
            Assert.Equal(res1, res2);
        }
    }
}
