using Microsoft.JSInterop;

namespace CatalogoWhatsApp.Services;

public class AuthService
{
    private readonly IJSRuntime _js;
    private bool _isAuthenticated = false;

    public AuthService(IJSRuntime js) => _js = js;

    public async Task<bool> Login(string user, string pass)
    {
        // Validación simple por ahora (puedes consultar a la DB luego)
        if (user == "admin" && pass == "admin123")
        {
            _isAuthenticated = true;
            await _js.InvokeVoidAsync("localStorage.setItem", "authToken", "TOKEN_SECRETO_ADMIN");
            return true;
        }
        return false;
    }

    public async Task<bool> IsAuthenticated()
    {
        var token = await _js.InvokeAsync<string>("localStorage.getItem", "authToken");
        return token == "TOKEN_SECRETO_ADMIN";
    }

    public async Task Logout()
    {
        await _js.InvokeVoidAsync("localStorage.removeItem", "authToken");
        _isAuthenticated = false;
    }
}