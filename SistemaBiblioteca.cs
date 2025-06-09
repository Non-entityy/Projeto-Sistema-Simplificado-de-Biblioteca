
public struct PeriodoEmprestimo
{
    public DateTime DataInicio;
    public DateTime? DataFim;

    public  PeriodoEmprestimo(DateTime dataInicio){
        DataInicio = dataInicio;
        DataFim = null;
    }
}

public class Pessoa
{
    public string? Nome { get; set; }
}
public class Usuario : Pessoa
{
    public string? Matricula { get; set; }
    public List<Emprestimo> EmprestimosAtivos {get; set;} = new List<Emprestimo>();
}
public class Livro
{
 public string? Nome {get; set;}
 public string? Autor{get; set;}
 public string? ISBN {get; set;}

 private int quantidadeDisponivel;
 public int QuantidadeDisponivel
 {
    get => quantidadeDisponivel;
    set
    {
        if (value < 0)
         throw new InvalidOperationException("Quantidade não pode ser negativa.");
            quantidadeDisponivel = value;
    }
 }
}
public class Emprestimo
{
    public Livro Livro {get; set;}
    public Usuario Usuario {get; set;}
    public PeriodoEmprestimo Periodo {get; set;}
    public bool Ativo => !Periodo.DataFim.HasValue;

  public void Finalizar()
{
    var periodo = Periodo;
    periodo.DataFim = DateTime.Now;
    Livro.QuantidadeDisponivel++;
}
}

 public class Biblioteca
    {
        public List<Livro> Livros { get; set; } = new List<Livro>();
        public List<Usuario> Usuarios { get; set; } = new List<Usuario>();
        public List<Emprestimo> Emprestimos { get; set; } = new List<Emprestimo>();

        public void CadastrarLivro()
        {
            Console.Write("Informe o título do livro: ");
            string titulo = Console.ReadLine();
            Console.Write("Informe o autor do livro: ");
            string autor = Console.ReadLine();
            Console.Write("Informe o ISBN do livro: ");
            string isbn = Console.ReadLine();
            Console.Write("Informe a quantidade de cópias disponíveis: ");
            int quantidade = int.Parse(Console.ReadLine());

            Livros.Add(new Livro { Nome = titulo, Autor = autor, ISBN = isbn, QuantidadeDisponivel = quantidade });
            Console.WriteLine("Livro cadastrado com sucesso!");
        }

        public void CadastrarUsuario()
        {
            Console.Write("Informe o nome do usuário: ");
            string nome = Console.ReadLine();
            Console.Write("Informe a matrícula do usuário: ");
            string matricula = Console.ReadLine();

            Usuarios.Add(new Usuario { Nome = nome, Matricula = matricula });
            Console.WriteLine("Usuário cadastrado com sucesso!");
        }

        public void RegistrarEmprestimo()
        {
            Console.Write("Informe o ISBN do livro para empréstimo: ");
            string isbn = Console.ReadLine();
            Console.Write("Informe a matrícula do usuário: ");
            string matricula = Console.ReadLine();

            var livro = Livros.FirstOrDefault(l => l.ISBN == isbn);
            var usuario = Usuarios.FirstOrDefault(u => u.Matricula == matricula);

            if (livro == null || usuario == null || livro.QuantidadeDisponivel == 0)
            {
                Console.WriteLine("Erro: livro ou usuário não encontrado, ou livro indisponível.");
                return;
            }

            livro.QuantidadeDisponivel--;
            var emprestimo = new Emprestimo { Livro = livro, Usuario = usuario, Periodo = new PeriodoEmprestimo(DateTime.Now) };
            Emprestimos.Add(emprestimo);
            usuario.EmprestimosAtivos.Add(emprestimo);

            Console.WriteLine("Empréstimo registrado com sucesso.");
        }

        public void RegistrarDevolucao()
        {
            Console.Write("Informe o ISBN do livro para devolução: ");
            string isbn = Console.ReadLine();
            Console.Write("Informe a matrícula do usuário: ");
            string matricula = Console.ReadLine();

            var emprestimo = Emprestimos.FirstOrDefault(e => e.Livro.ISBN == isbn && e.Usuario.Matricula == matricula && e.Ativo);

            if (emprestimo == null)
            {
                Console.WriteLine("Empréstimo não encontrado ou já finalizado.");
                return;
            }

            emprestimo.Finalizar();
            emprestimo.Usuario.EmprestimosAtivos.Remove(emprestimo);
            Console.WriteLine("Devolução registrada com sucesso!");
        }

        public void ListarLivros()
        {
            Console.WriteLine("==== Lista de Livros ====");
            foreach (var livro in Livros)
            {
                Console.WriteLine($"Título: {livro.Nome}, Autor: {livro.Autor}, ISBN: {livro.ISBN}, Disponíveis: {livro.QuantidadeDisponivel}");
            }
        }

        public void Relatorio()
        {
            Console.WriteLine("==== Relatório de Empréstimos ====");
        Console.WriteLine(" ");
            Console.WriteLine("-- Livros Emprestados:");
            foreach (var emprestimo in Emprestimos.Where(e => e.Ativo))
            {
             Console.WriteLine($"Título: {emprestimo.Livro.Nome}, Usuário: {emprestimo.Usuario.Nome}, Data Empréstimo: {emprestimo.Periodo.DataInicio}");  
            }
        Console.WriteLine(" ");
            Console.WriteLine("-- Livros Disponíveis:");
            foreach (var livro in Livros.Where(l => l.QuantidadeDisponivel > 0))
            {
                Console.WriteLine($"Título: {livro.Nome}, Autor: {livro.Autor}, ISBN: {livro.ISBN}, Disponíveis: {livro.QuantidadeDisponivel}");
            }
        Console.WriteLine(" ");
            Console.WriteLine("-- Livros Devolvidos: ");
            foreach (var devolvido in Emprestimos.Where(e => !e.Ativo))
            {       
            Console.WriteLine($"Título: {devolvido.Livro.Nome}, Usuário: {devolvido.Usuario.Nome}, Data Empréstimo: {devolvido.Periodo.DataInicio:dd/MM/yyyy}, Data Devolução: {devolvido.Periodo.DataFim:dd/MM/yyyy}");
            }
        Console.WriteLine(" ");
            Console.WriteLine("-- Usuários com livros emprestados:");
            foreach (var usuario in Usuarios)
            {
                var livrosEmprestados = usuario.EmprestimosAtivos.Where(e => e.Ativo).ToList();
                if (livrosEmprestados.Any())
                {
                    Console.WriteLine($"Usuário: {usuario.Nome} - Matrícula: {usuario.Matricula}");
                    foreach (var emprestimo in livrosEmprestados)
                    {
                        Console.WriteLine($"  - Livro: {emprestimo.Livro.Nome}, Data Empréstimo: {emprestimo.Periodo.DataInicio}");
                    }
                }
            }
        }
    }