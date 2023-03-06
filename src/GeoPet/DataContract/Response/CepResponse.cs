namespace GeoPet.DataContract.Response;

public class CepResponse
{
    public string Cep { get; set; } = null!;
    public string Logradouro { get; set; } = null!;
    public string Complemento { get; set; } = null!;
    public string Bairro { get; set; } = null!;
    public string Localidade { get; set; } = null!;
    public string Uf { get; set; } = null!;
    public string Ibge { get; set; } = null!;
    public string Ddd { get; set; } = null!;
    public string Siafi { get; set; } = null!;

}