using BoardCommon.Snowflake;
using System.Diagnostics;

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


    [Fact]
    public async Task NextId_ShouldBeUniqueAndIncreasing()
    {
        // given
        int repeatCount = 1000;
        int idCount = 1000;
        var tasks = new List<Task<List<long>>>();

        // when
        for (int i = 0; i < repeatCount; i++)
        {
            tasks.Add(Task.Run(() => GenerateIdList(_idGenerator, idCount)));
        }

        // then
        var result = new List<long>();
        var results = await Task.WhenAll(tasks);

        foreach (var idList in results)
        {
            for (int i = 1; i < idList.Count; i++)
            {
                Assert.True(idList[i] > idList[i - 1]);
            }
            result.AddRange(idList);
        }

        Assert.Equal(repeatCount * idCount, result.Distinct().Count());
    }

    private static List<long> GenerateIdList(SnowflakeIdGenerator generator, int count)
    {
        var idList = new List<long>();
        for (int i = 0; i < count; i++)
        {
            idList.Add(generator.NextId());
        }
        return idList;
    }

    [Fact]
    public async Task NextId_PerformanceTest()
    {
        // given
        int repeatCount = 1000;
        int idCount = 1000;
        var semaphore = new SemaphoreSlim(0, repeatCount);

        // when
        var stopwatch = Stopwatch.StartNew();
        var tasks = new List<Task>();

        for (int i = 0; i < repeatCount; i++)
        {
            tasks.Add(Task.Run(() =>
            {
                GenerateIdList(_idGenerator, idCount);
                semaphore.Release();
                return Task.CompletedTask;
            }));
        }

        // 모든 작업이 끝날 때까지 대기
        await semaphore.WaitAsync();
        await Task.WhenAll(tasks);
        stopwatch.Stop();

        Console.WriteLine($"Execution Time: {stopwatch.ElapsedMilliseconds} ms");
    }
}
