using api_filmes_senai.Domains;

namespace api_filmes_senai.Interface
{
    /// <summary>
    /// Interface para genero : contrato
    /// Toda classe que herdar (implementar) essa ubterface, 
    /// deverá implementar todos os métodos definidos aqui dentro
    /// </summary>
    public interface IGeneroRepository
    {
        //CRUD : Métodos
        //C : Create: Cadastrar um novo objeto
        //R : Read: Listar todos os objetos
        //U : Update: Alterar um objeto
        //D : Delete: Deleto ou excluo um objeto

        // Método = TipoDeRetorno NomeDoMetodo(Argumentos ou Parametros)

        void Cadastrar(Genero novoGenero);

        List<Genero> Listar();

        void Atualizar(Guid id, Genero genero);

        void Deletar (Guid id);

        Genero BuscarPorId(Guid id);

    }
}
