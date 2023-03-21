using FilmFlow.Domain;

namespace FilmFlow.Tests
{
	public class CryptoTest
	{
		[Fact]
		public void TestHashGenerator()
		{
			const string testString = "test secure hash avans";
			const string expectedHash = "c36d07deffe124a2584ceedb53342289491b8fb727d1677cd224fa6d68f9690f";

			var hash = Crypto.GenerateHash(testString);

			Assert.Equal(expectedHash, hash);
		}

		[Fact]
		public void TestRandomStringIsRandomAndBaseEncoded()
		{
			var baseEncodedString = Crypto.GenerateRandomBaseEncodedString();

			Assert.True(baseEncodedString.Length > 10);
			Assert.EndsWith("=", baseEncodedString);
		}
	}
}
