namespace BoardCommon.Snowflake;

public class SnowflakeIdGenerator
{
    private static readonly long Epoch = new DateTime(2020, 1, 1, 0, 0, 0, DateTimeKind.Utc).Ticks / TimeSpan.TicksPerMillisecond;
    private const int NodeIdBits = 10;
    private const int SequenceBits = 12;
    private const long MaxNodeId = (1L << NodeIdBits) - 1;
    private const long MaxSequence = (1L << SequenceBits) - 1;

    private readonly long _nodeId;
    private long _lastTimestamp = -1L;
    private long _sequence = 0L;
    private readonly object _lock = new();

    public SnowflakeIdGenerator(long nodeId)
    {
        if (nodeId < 0 || nodeId > MaxNodeId)
            throw new ArgumentException($"Node ID must be between 0 and {MaxNodeId}");

        _nodeId = nodeId;
    }

    public long NextId()
    {
        lock (_lock)
        {
            long timestamp = GetCurrentTimestamp();

            if (timestamp < _lastTimestamp)
            {
                throw new InvalidOperationException("Clock moved backwards. Refusing to generate ID.");
            }

            if (timestamp == _lastTimestamp)
            {
                _sequence = (_sequence + 1) & MaxSequence;
                if (_sequence == 0)
                {
                    // 시퀀스가 오버플로우 되면 다음 밀리초를 기다림
                    timestamp = WaitNextMillis(_lastTimestamp);
                }
            }
            else
            {
                _sequence = 0;
            }

            _lastTimestamp = timestamp;

            return ((timestamp - Epoch) << (NodeIdBits + SequenceBits)) |
                   (_nodeId << SequenceBits) |
                   _sequence;
        }
    }

    private long GetCurrentTimestamp()
    {
        return DateTime.UtcNow.Ticks / TimeSpan.TicksPerMillisecond;
    }

    private long WaitNextMillis(long lastTimestamp)
    {
        long timestamp = GetCurrentTimestamp();
        while (timestamp <= lastTimestamp)
        {
            timestamp = GetCurrentTimestamp();
        }
        return timestamp;
    }
}
