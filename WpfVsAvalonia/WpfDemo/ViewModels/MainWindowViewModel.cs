using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace WpfDemo.ViewModels;

public class MainWindowViewModel:ObservableObject
{
    private bool _status;
     public bool Status
    {
        get => _status;
        set => SetProperty(ref _status, value);
    }
    public ICommand ChangeStatusCommand { get; set; }

    public MainWindowViewModel()
    {
        _status = false;
        ChangeStatusCommand= new RelayCommand(ChangeStatus);
    }
    private void ChangeStatus()
    {
        Status=!Status;
    }
}