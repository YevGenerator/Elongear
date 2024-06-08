namespace Elongear.Pages.Views;

public partial class ConfirmationDigits : ContentView
{
    public static readonly BindableProperty DigitsProperty =
        BindableProperty.Create(nameof(Digits), typeof(string), typeof(ConfirmationDigits), string.Empty);

    public ConfirmationDigits()
	{
		InitializeComponent();
        Digit1.Focus();
	}
    public string Digits
    {
        get => (string)GetValue(DigitsProperty);
        set => SetValue(DigitsProperty, value);
    }

    private void OnTextChanged(object sender, TextChangedEventArgs e)
    {
        if (sender is not Entry currentEntry) return;
        Digits = Digit1.Text + Digit2.Text + Digit3.Text + Digit4.Text + Digit5.Text;
        if (e.NewTextValue.Length !=0)
        {
            switch (currentEntry)
            {
                case var _ when Equals(currentEntry, Digit1):
                    Digit2.Focus();
                    break;
                case var _ when Equals(currentEntry, Digit2):
                    Digit3.Focus();
                    break;
                case var _ when Equals(currentEntry, Digit3):
                    Digit4.Focus();
                    break;
                case var _ when Equals(currentEntry, Digit4):
                    Digit5.Focus();
                    break;
            }
        }
    }

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        //HiddenEntry.Focus();
    }
}