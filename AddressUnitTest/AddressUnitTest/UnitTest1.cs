namespace AddressUnitTest
{
    public class UnitTest1
    {
        public class AddressGeneratorTests
        {
            [Fact]
            public void Constructor_WithInvalidBitNumber_ThrowsArgumentOutOfRangeException()
            {
                // Arrange
                byte[] ipv4Bytes = new byte[] { 192, 168, 0, 1 };
                int bitNumber = -1;

                // Act & Assert
                Assert.Throws<ArgumentOutOfRangeException>(() => new AddressGenerator(bitNumber, ipv4Bytes));
            }

            [Fact]
            public void Constructor_WithInvalidIpv4Bytes_ThrowsArgumentException()
            {
                // Arrange
                byte[] ipv4Bytes = new byte[] { 192, 168, 0 };
                int bitNumber = 24;

                // Act & Assert
                Assert.Throws<ArgumentException>(() => new AddressGenerator(bitNumber, ipv4Bytes));
            }

            [Fact]
            public void GenerateIPv4_WithValidInputs_ReturnsExpectedResult()
            {
                // Arrange
                byte[] ipv4Bytes = new byte[] { 192, 168, 0, 1 };
                int bitNumber = 24;
                string expected = "192.168.0.0";

                var addressGenerator = new AddressGenerator(bitNumber, ipv4Bytes);

                // Act
                string actual = addressGenerator.generateIPv4();

                // Assert
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void GenerateSubnet_WithValidInputs_ReturnsExpectedResult()
            {
                // Arrange
                byte[] ipv4Bytes = new byte[] { 192, 168, 0, 1 };
                int bitNumber = 24;
                string expected = "255.255.255.0/24";

                var addressGenerator = new AddressGenerator(bitNumber, ipv4Bytes);

                // Act
                string actual = addressGenerator.generateSubnet();

                // Assert
                Assert.Equal(expected, actual);
            }
        }
    }
}