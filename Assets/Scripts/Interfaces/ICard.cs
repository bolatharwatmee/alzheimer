public interface ICard
{
    void Flip();
    bool IsMatched { get; set; }
    void SetMatched(bool matched);
}
