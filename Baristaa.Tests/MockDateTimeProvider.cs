using Baristaa.Core.Providers;

namespace Baristaa.Core.Handlers.Tests;

public class MockDateTimeProvider : IDateTimeProvider
{
    private readonly DateTime _fakeDate;

    public MockDateTimeProvider(DateTime fakeDate)
    {
        _fakeDate = fakeDate;
    }

    public DateTime GetCurrentDateTime()
    {
        return _fakeDate;
    }
}