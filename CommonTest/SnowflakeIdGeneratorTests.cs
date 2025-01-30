using BoardCommon.Snowflake;

namespace CommonTest;

public class SnowflakeIdGeneratorTests
{
    private readonly SnowflakeIdGenerator _idGenerator;

    public SnowflakeIdGeneratorTests()
    {
        _idGenerator = new SnowflakeIdGenerator(1);
    }

    [Fact]
    public void GenerateUniqueIds_ShouldReturnDifferentValues()
    {
        long id1 = _idGenerator.NextId();
        long id2 = _idGenerator.NextId();

        Assert.NotEqual(id1, id2);
    }

    [Fact]
    public void GenerateId_ShouldBePositive()
    {
        long id = _idGenerator.NextId();
        Assert.True(id > 0);
    }

    [Fact]
    public void GenerateId_ShouldIncreaseOverTime()
    {
        long id1 = _idGenerator.NextId();
        System.Threading.Thread.Sleep(1);
        long id2 = _idGenerator.NextId();

        Assert.True(id2 > id1);
    }

    [Fact]
    public void DecodeId_ShouldExtractCorrectValues()
    {
        long id = _idGenerator.NextId();
        var (timestamp, nodeId, sequence) = SnowflakeIdParser.Decode(id);

        Assert.True(timestamp > new DateTime(2020, 1, 1));
        Assert.Equal(1, nodeId);
        Assert.True(sequence >= 0);
    }
}
