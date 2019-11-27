using System.Windows.Forms;
using Maze.Engine.Input;
using Xunit;

namespace Maze.Tests.Engine
{
    public class ClientFormInputTest
    {
        private ClientFormInput _input;

        public ClientFormInputTest()
        {
            _input = new ClientFormInput();
        }

        [Fact]
        public void TestShouldNotBeInitializedUntilUserIdIsSet()
        {
            Assert.False(_input.IsInitialized());

            _input.SetUserId(1);

            Assert.True(_input.IsInitialized());
        }

        [Fact]
        public void TestReportsKeyAsPressedAfterKeyDown()
        {
            _input.SetUserId(1);

            _input.KeyDown(Keys.Enter);

            Assert.True(_input.IsKeyDown(Keys.Enter));
        }

        [Fact]
        public void TestNoLongerReportsKeyAsPressedAfterKeyUp()
        {
            _input.SetUserId(1);

            _input.KeyDown(Keys.Enter);

            Assert.True(_input.IsKeyDown(Keys.Enter));

            _input.KeyUp(Keys.Enter);

            Assert.False(_input.IsKeyDown(Keys.Enter));
        }

        [Fact]
        public void TestTracksKeyPressesByUser()
        {
            _input.SetUserId(1);
            _input.SetUserId(2);

            _input.KeyDown(Keys.Enter);

            Assert.True(_input.IsKeyDown(Keys.Enter));
            Assert.False(_input.IsUserKeyDown(1, Keys.Enter));
        }

        [Fact]
        public void TestOverridesWithPressedKeysFromSeparateInstance()
        {
            var otherInput = new ClientFormInput();

            otherInput.SetUserId(1);
            _input.SetUserId(1);

            otherInput.KeyDown(Keys.Up);
            _input.KeyDown(Keys.Enter);

            _input.Merge(otherInput);

            Assert.False(_input.IsKeyDown(Keys.Enter));
            Assert.True(_input.IsKeyDown(Keys.Up));
        }
    }
}
