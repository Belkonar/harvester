namespace harvester.Data;

public interface IDto<T>
{
    static abstract T Map();
}