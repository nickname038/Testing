using System;
using Xunit;
using IIG.BinaryFlag;
using IIG.CoSFE.DatabaseUtils;


namespace Lab4
{
    public class BinaryFlag_DB
    {
        private const string Server = @"DESKTOP-AKKATJN\SQLEXPRESS";
        private const string Database = @"IIG.CoSWE.FlagpoleDB";
        private const bool isTrusted = false;
        private const string Login = @"sa";
        private const string Password = @"2791";
        private const int ConnectionTimeout = 75;
        private static FlagpoleDatabaseUtils db
            = new FlagpoleDatabaseUtils(Server, Database, isTrusted, Login, Password, ConnectionTimeout);

        private int GetMaxId()
        {
            return db.GetIntBySql("select max(MultipleBinaryFlagID) from MultipleBinaryFlags").Value;
        }

        [Theory]

        [InlineData(2, true)]
        [InlineData(33, false)]
        [InlineData(65, false)]
        [InlineData(1234567890, true)]
        public void shouldReturnCorrectResultsForInitValue(ulong lenght, bool initValue)
        {
            var obj = new MultipleBinaryFlag(lenght, initValue);
            var view = obj.ToString();
            var value = initValue;
            Assert.True(db.AddFlag(view, value));
            var id = GetMaxId();
            Assert.True(db.GetFlag(id, out string outView, out bool? outValue));
            Assert.Equal(outView, view);
            Assert.Equal<bool?>(outValue, value);
        }

        [Theory]
        [InlineData(3, false, new ulong[3] { 0, 1, 2 })]
        [InlineData(12345, false, new ulong[1] { 22 })]
        [InlineData(11, false, new ulong[6] { 0, 1, 3, 6, 10, 2 })]
        [InlineData(65, false, new ulong[4] { 0, 0, 5, 64 })]
        public void shouldReturnCorrectResultsForChangedValue_False(ulong lenght, bool initValue, ulong[] positions)
        {
            var obj = new MultipleBinaryFlag(lenght, initValue);
            foreach (ulong position in positions)
                obj.SetFlag(position);

            var view = obj.ToString();
            var value = (bool)obj.GetFlag();
            Assert.True(db.AddFlag(view, value));
            var id = GetMaxId();
            Assert.True(db.GetFlag(id, out string outView, out bool? outValue));
            Assert.Equal(outView, view);
            Assert.Equal<bool?>(outValue, value);
        }

        [Theory]
        [InlineData(3, true, new ulong[0] {})]
        [InlineData(12345, true, new ulong[1] { 22 })]
        [InlineData(11, true, new ulong[6] { 0, 1, 3, 6, 10, 2 })]
        [InlineData(65, true, new ulong[4] { 0, 0, 5, 64 })]
        public void shouldReturnCorrectResultsForChangedValue_True(ulong lenght, bool initValue, ulong[] positions)
        {
            var obj = new MultipleBinaryFlag(lenght, initValue);
            foreach (ulong position in positions)
                obj.ResetFlag(position);

            var view = obj.ToString();
            var value = (bool)obj.GetFlag();
            Assert.True(db.AddFlag(view, value));
            var id = GetMaxId();
            Assert.True(db.GetFlag(id, out string outView, out bool? outValue));
            Assert.Equal(outView, view);
            Assert.Equal<bool?>(outValue, value);
        }

        [Theory]
        [InlineData("", true)]
        [InlineData("", false)]
        [InlineData("F", false)]
        [InlineData("T", true)]
        [InlineData("abracadabra", true)]
        [InlineData("abracadabra", false)]

        public void shouldNotWriteToDBInvalidData(string view, bool value)
        {
            Assert.False(db.AddFlag(view, value));
        }
    }
}