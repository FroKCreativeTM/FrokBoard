namespace BoardCommon.Snowflake;

public class SnowflakeIdParser
{
    private static readonly long Epoch = new DateTime(2020, 1, 1, 0, 0, 0, DateTimeKind.Utc).Ticks / TimeSpan.TicksPerMillisecond;
    private const int NodeIdBits = 10;
    private const int SequenceBits = 12;

    public static (DateTime Timestamp, long NodeId, long Sequence) Decode(long id)
    {
        long timestamp = (id >> (NodeIdBits + SequenceBits)) + Epoch;
        long nodeId = (id >> SequenceBits) & ((1L << NodeIdBits) - 1);
        long sequence = id & ((1L << SequenceBits) - 1);

        return (new DateTime(timestamp * TimeSpan.TicksPerMillisecond, DateTimeKind.Utc), nodeId, sequence);
    }
}
