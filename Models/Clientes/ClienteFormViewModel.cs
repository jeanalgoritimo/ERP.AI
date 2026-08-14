using System.ComponentModel.DataAnnotations;

namespace ERP.AI.Models.Clientes;

public sealed class ClienteFormViewModel
{
    public Guid? Id { get; set; }

    [Required(ErrorMessage = "Informe o código.")]
    [StringLength(20)]
    [Display(Name = "Código")]
    public string Codigo { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe a razão social.")]
    [StringLength(160)]
    [Display(Name = "Razão social")]
    public string RazaoSocial { get; set; } = string.Empty;

    [StringLength(120)]
    [Display(Name = "Nome fantasia")]
    public string NomeFantasia { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe o CPF ou CNPJ.")]
    [StringLength(18)]
    [Display(Name = "CPF/CNPJ")]
    public string Documento { get; set; } = string.Empty;

    [EmailAddress(ErrorMessage = "Informe um e-mail válido.")]
    [StringLength(160)]
    [Display(Name = "E-mail")]
    public string Email { get; set; } = string.Empty;

    [StringLength(20)]
    public string Telefone { get; set; } = string.Empty;

    [StringLength(120)]
    public string Cidade { get; set; } = string.Empty;

    [StringLength(2, MinimumLength = 2, ErrorMessage = "Use a sigla da UF com 2 letras.")]
    [Display(Name = "UF")]
    public string Estado { get; set; } = string.Empty;

    [Required]
    public string Categoria { get; set; } = "Regular";

    [Range(0, 999_999_999, ErrorMessage = "Informe um limite válido.")]
    [Display(Name = "Limite de crédito")]
    public decimal LimiteCredito { get; set; }

    public bool Ativo { get; set; } = true;
}
