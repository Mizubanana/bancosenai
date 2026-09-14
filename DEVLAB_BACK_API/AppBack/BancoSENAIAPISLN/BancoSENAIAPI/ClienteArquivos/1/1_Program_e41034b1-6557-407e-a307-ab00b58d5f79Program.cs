using System;

class Program
{
    static void Main(string[] args)
    {
        int homens = 0;
        int mulheres = 0;

        for (int i = 1; i <= 4; i++)
        {
            Console.Write("Digite o nome da " + i + "ª pessoa: ");
            string nome = Console.ReadLine();

            Console.Write("Digite o sexo da " + i + "ª pessoa (M/F): ");
            string sexo = Console.ReadLine().ToUpper();

            if (sexo == "M")
            {
                homens++;
            }
            else if (sexo == "F")
            {
                mulheres++;
            }
            else
            {
                Console.WriteLine("Sexo inválido! Será desconsiderado.");
            }
        }

        Console.WriteLine("\nQuantidade de homens: " + homens);
        Console.WriteLine("Quantidade de mulheres: " + mulheres);
    }
}
