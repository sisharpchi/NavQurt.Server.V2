using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NavQurt.Server.App.Models;

public class UserDto : INotifyPropertyChanged
{
    private long _id;
    private string? _firstName;
    private string? _lastName;
    private string? _userName;
    private string? _email;
    private string? _phoneNumber;
    private string? _role;
    private string? _newRole;

    public long Id
    {
        get => _id;
        set => SetProperty(ref _id, value);
    }

    public string? FirstName
    {
        get => _firstName;
        set => SetProperty(ref _firstName, value);
    }

    public string? LastName
    {
        get => _lastName;
        set => SetProperty(ref _lastName, value);
    }

    public string? UserName
    {
        get => _userName;
        set => SetProperty(ref _userName, value);
    }

    public string? Email
    {
        get => _email;
        set => SetProperty(ref _email, value);
    }

    public string? PhoneNumber
    {
        get => _phoneNumber;
        set => SetProperty(ref _phoneNumber, value);
    }

    public string? Role
    {
        get => _role;
        set => SetProperty(ref _role, value);
    }

    public string? NewRole
    {
        get => _newRole;
        set => SetProperty(ref _newRole, value);
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = "")
    {
        if (EqualityComparer<T>.Default.Equals(storage, value))
        {
            return false;
        }

        storage = value;
        OnPropertyChanged(propertyName);
        return true;
    }

    protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
