class Door(int passcode)
{
    private DoorState _state = DoorState.Locked;
    private int _passcode = passcode;

    public DoorState State => _state;

    public void Open()
    {
        if (_state == DoorState.Closed)
            _state = DoorState.Open;
    }

    public void Close()
    {
        if (_state == DoorState.Open)
            _state = DoorState.Closed;
    }

    public void Lock()
    {
        if (_state == DoorState.Closed)
            _state = DoorState.Locked;
    }

    public void Unlock(int passcode)
    {
        if (_state == DoorState.Locked && passcode == _passcode)
            _state = DoorState.Closed;
    }

    public void ChangePasscode(int oldPasscode, int newPasscode)
    {
        if (oldPasscode == _passcode)
            _passcode = newPasscode;
    }
}

enum DoorState { Locked, Open, Closed };