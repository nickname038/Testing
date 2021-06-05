using System;
using Xunit;
using IIG.BinaryFlag;

namespace Lab3
{
    public class UnitTest1
    {
        //Constructor
        [Theory]

        [InlineData(0, true)]
        [InlineData(1, false)]
        public void shouldThrowErrorWithTooSmallLengthParametr_Constructor(ulong lenght, bool initValue)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new MultipleBinaryFlag(lenght, initValue));
        }

        [Fact]
        public void shouldThrowErrorWithTooLargeLengthParametr_Constructor()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new MultipleBinaryFlag(17179868705));
        }

        [Theory]

        [InlineData(2, true)]
        [InlineData(33, false)]
        [InlineData(65, false)]
        public void shouldReturnNotNullWithCorrectLengthParametr_Constructor(ulong lenght, bool initValue)
        {
            var obj = new MultipleBinaryFlag(lenght, initValue);
            Assert.NotNull(obj);
        }

        [Theory]

        [InlineData(2, true, "TT")]
        [InlineData(33, false, "FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF")]
        [InlineData(65, false, "FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF")]
        public void shouldSetCorrectFlagValues_Constructor(ulong lenght, bool initValue, string result)
        {
            var obj = new MultipleBinaryFlag(lenght, initValue);
            Assert.Equal(obj.ToString(), result);
        }

        //Dispose
        [Theory]

        [InlineData(2, true)]
        [InlineData(33, false)]
        [InlineData(65, false)]
        public void shouldDisposeFlags_Dispose(ulong lenght, bool initValue)
        {
            var obj = new MultipleBinaryFlag(lenght, initValue);
            obj.Dispose();
            Assert.Null(obj.ToString());
        }

        [Theory]

        [InlineData(2, true)]
        [InlineData(33, false)]
        [InlineData(65, false)]
        public void shouldIgnoreSecondCall_Dispose(ulong lenght, bool initValue)
        {
            var obj = new MultipleBinaryFlag(lenght, initValue);
            obj.Dispose();
            obj.Dispose();
            Assert.Null(obj.ToString());
        }

        //SetFlags

        [Theory]

        [InlineData(2, true, 1)]
        [InlineData(33, false, 56)] //значення за межами інтервалу
        [InlineData(65, false, 40)]
        public void shouldReturnIfFlagObjIsNull_SetFlag(ulong lenght, bool initValue, ulong position)
        {
            var obj = new MultipleBinaryFlag(lenght, initValue);
            obj.Dispose();
            obj.SetFlag(position);
            Assert.Null(obj.ToString());
        }

        [Theory]

        [InlineData(2, true, 2)]
        [InlineData(33, false, 56)]
        [InlineData(65, false, 98)]
        public void shouldThrowErrorWithInvalidPosition_SetFlag(ulong lenght, bool initValue, ulong position)
        {
            var obj = new MultipleBinaryFlag(lenght, initValue);
            Assert.Throws<ArgumentOutOfRangeException>(() => obj.SetFlag(position));
        }

        [Theory]

        [InlineData(3, false, new ulong[1] { 2 }, "FFT")]
        [InlineData(3, true, new ulong[1] { 2 }, "TTT")]
        [InlineData(33, false, new ulong[3] { 0, 1, 31 }, "TTFFFFFFFFFFFFFFFFFFFFFFFFFFFFFTF")]
        [InlineData(65, false, new ulong[4] { 0, 0, 5, 64 }, "TFFFFTFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFT")]
        public void shouldSetValueAtPositionToTrue_SetFlag(ulong lenght, bool initValue, ulong[] positions, string result)
        {
            var obj = new MultipleBinaryFlag(lenght, initValue);
            foreach (ulong position in positions)
                obj.SetFlag(position);

            Assert.Equal(obj.ToString(), result);
        }

        //ResetFlag
        [Theory]

        [InlineData(2, true, 1)]
        [InlineData(33, false, 56)]
        [InlineData(65, false, 40)]
        public void shouldReturnIfFlagObjIsNull_ResetFlag(ulong lenght, bool initValue, ulong position)
        {
            var obj = new MultipleBinaryFlag(lenght, initValue);
            obj.Dispose();
            obj.ResetFlag(position);
            Assert.Null(obj.ToString());
        }

        [Theory]

        [InlineData(2, false, 2)]
        [InlineData(33, true, 56)]
        [InlineData(65, false, 98)]
        public void shouldThrowErrorWithInvalidPosition_ResetFlag(ulong lenght, bool initValue, ulong position)
        {
            var obj = new MultipleBinaryFlag(lenght, initValue);
            Assert.Throws<ArgumentOutOfRangeException>(() => obj.ResetFlag(position));
        }

        [Theory]
        [InlineData(3, true, new ulong[1] { 2 }, "TTF")]
        [InlineData(3, false, new ulong[1] { 2 }, "FFF")]
        [InlineData(33, true, new ulong[3] { 0, 1, 31 }, "FFTTTTTTTTTTTTTTTTTTTTTTTTTTTTTFT")]
        [InlineData(65, true, new ulong[4] { 0, 0, 5, 64 }, "FTTTTFTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTF")]
        public void shouldSetValueAtPositionToFalse_ResetFlag(ulong lenght, bool initValue, ulong[] positions, string result)
        {
            var obj = new MultipleBinaryFlag(lenght, initValue);
            foreach (ulong position in positions)
                obj.ResetFlag(position);

            Assert.Equal(obj.ToString(), result);
        }

        //GetFlag

        [Theory]

        [InlineData(2, true)]
        [InlineData(33, false)]
        [InlineData(65, false)]
        public void shouldReturnNullIfFlagObjIsNull_GetFlag(ulong lenght, bool initValue)
        {
            var obj = new MultipleBinaryFlag(lenght, initValue);
            obj.Dispose();
            Assert.Null(obj.GetFlag());
        }

        [Theory]

        [InlineData(2, true)]
        [InlineData(33, false)]
        [InlineData(65, false)]
        public void shouldReturnInitBool_GetFlag(ulong lenght, bool initValue)
        {
            var obj = new MultipleBinaryFlag(lenght, initValue);
            if (initValue)
            {
                Assert.True(obj.GetFlag());
            } else
            {
                Assert.False(obj.GetFlag());
            }
        }

        [Theory]

        [InlineData(2, true)]
        [InlineData(33, true)]
        [InlineData(65, true)]
        public void shouldReturnFalseifResetFlagWasCalled_GetFlag(ulong lenght, bool initValue)
        {
            var obj = new MultipleBinaryFlag(lenght, initValue);
            obj.ResetFlag(1);
            Assert.False(obj.GetFlag());
        }

        [Theory]

        [InlineData(2, false)]
        [InlineData(33, false)]
        [InlineData(65, false)]
        public void shouldReturnTrueifAllFlagsWasSetToTrue_GetFlag(ulong lenght, bool initValue)
        {
            var obj = new MultipleBinaryFlag(lenght, initValue);
            for (ulong i = 0; i < lenght; i++)
                obj.SetFlag(i);

            Assert.True(obj.GetFlag());
        }
    }
}
