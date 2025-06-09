using System;
using System.Collections.Generic;
using System.Linq;

namespace SistemaBiblioteca{

    class Program
    {
        static void Main(string[] args)
        {
            Biblioteca biblioteca = new Biblioteca();
            int opcao;

            do
            {
                Console.WriteLine("\n==== Menu Biblioteca ====");
                Console.WriteLine("1. Cadastrar Livro");
                Console.WriteLine("2. Cadastrar Usuário");
                Console.WriteLine("3. Registrar Empréstimo");
                Console.WriteLine("4. Registrar Devolução");
                Console.WriteLine("5. Listar Livros");
                Console.WriteLine("6. Exibir Relatórios");
                Console.WriteLine("0. Sair");
                Console.Write("Escolha: ");
                opcao = int.Parse(Console.ReadLine());

                Console.WriteLine();

                switch (opcao)
                {
                    case 1: biblioteca.CadastrarLivro(); break;
                    case 2: biblioteca.CadastrarUsuario(); break;
                    case 3: biblioteca.RegistrarEmprestimo(); break;
                    case 4: biblioteca.RegistrarDevolucao(); break;
                    case 5: biblioteca.ListarLivros(); break;
                    case 6: biblioteca.Relatorio(); break;
                    case 0: Console.WriteLine("Saindo..."); break;
                    default: Console.WriteLine("Opção inválida."); break;
                }
            } while (opcao != 0);


        }
        }



}