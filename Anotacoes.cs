//Modelo Anêmico:
	//É um model que, em sua essência, só tem propriedades "get/set" públicas e pouco ou 	nenhum 	comportamento.
	//Validações e comportamentos são feitos à partir de uma classe de serviço.

	// É um modelo de arquitetura que tem algumas desvantagens:
	// 	- Violação do encapsulamento
	// 	- Dificuldade na manutenção
	// 	- Geralmente a lógica de negócio pode ser duplicada
	// 	- Não é possível garantir entidades no model que sejam consistentes
	// 	- Baixa coesão (sem responsabilidades bem definidas)

	// Como contornar esses problemas?
	// 	- Usar propriedades com "setters" privados e as classe como "sealed"(que não 		podem usar herança)
	// 	- Validar estado da entidade na própria entidade e não em serviços
	// 	- Evitar construtores sem parâmetros, sempre exigir dados obrigatórios para 		fazer da entidade, única
	// 	- Definir invariantes, propriedades que são obrigatórias para definir a entidade
	// 	- Regras de comportamente no model e não no serviço
	// 	- Usar o conceito de Programação orientada a objetos
	// 	- Cuidado e atenção ao usar ferramentas ORM (EF Core)

	// Exemplo de como enriquecer uma classe anêmica:
		public class Cliente
		{
			public int Id{get;set;}
			public string Nome{get;set;}
			public string Endereco{get;set;}
		}

		//Se tornará =>
		
		public sealed class Cliente //Usando o sealed para que ninguém possa herdar
		{
			public int Id{get; private set;}
			public string Nome{get; private set;}
			public string Endereco{get; private set;}

			//Construtor com parâmetros obrigatórios para definir a entidade única
			public Cliente(int id, string nome, string endereco)
			{
				Validar(id, nome, endereco);
				Id = id;
				Nome = nome;
				Endereco = endereco;
			}

			//
			public void Updade(int id, string nome, string endereco)
			{
				Validar(id, nome, endereco);
				Nome = nome;
				Endereco = endereco;
			}

			private static void Validar(int id, string nome, string endereco)
			{
				if (id < 0)
					throw new InvalidOperationException("O Id tem que ser maior que 0");

				if (string.IsNullOrEmpty(nome) || string.IsNullOrEmpty(endereco))
					throw new InvalidOperationException("O nome e enndereço são obrigatórios");
			}	
		}

//Separation of concerns - Separação de responsabilidade

	//Não se deve misturar conceitos e/ou responsabilidades diferentes dentro do 
	//código de um projeto de software

	//A violação deste princípio causa código duplicado com mais de uma responsabilidade
	//e também viola os princípios de SOLID da Responsabilidade Única(SRP) e o
	//princípio DRY(Don't repeat yourself)

	//Exemplos de aplicação desse conceito:
		//-Separar a interface do usuário(front-end) da lógica de negócios(back-end)
		//-Separar o código de acesso a dados do código de apresentação de dados
		//-Separar o projeto em diferentes módutos/camadas com responsabilidades distintas
		//-Criar componentes/classes/funções que realizam apenas um única tarefa com eficiência	

	//A aplicação da Separação de Responsabilidades envolve 2 processos:
			//1 - Reduzir o acoplamento
			//2 - Aumentar a coesão

			//Acoplamento:
				//Acoplamento é o nível de dependência que pode existir entre os 
				//componentes do sistema; Quanto maior o acoplamento entre os componentes
				//do sistema, maior será a dependência entre eles, e mais difícil será
				//manter, reusar e estender o sistema.

			//Coesão:
				//Coesão é o nível de integridade interna dos componentes do sistema;
				//Quanto maior a coesão entre os componentes, mais definidas são suas
				//responsabilidades, sendo mais difícil desmembrar o componente em 
				//outros componentes.

	//Vantagens de aplicar Separação de Responsabilidades:
		// 1 - Facilita a manutenção.
		// 2 - Melhor reutilização de código
		// 3 - Melhor clareza no código	
		// 4 - Melhora a testabilidade
		// 5 - Permite a evolução mais rápida do projeto

//Dependency Inversion(DI) ou Inversão da Dependência
	//A direção da dependência em uma aplicação deverá ser na direção da abstração
	//e não nos detalhes de implementação.

	//Módulos de alto nível não devem depender de módulos de baixo nível. ambos devem
	//depender de uma abstração.

	//Abstrações não devem depender de detalhes. Detalhes devem depender de abstrações.

		//Módulos de alto nível são classes da camada de negócio, que possuem lógica mais complexa.
		//Módulos de baixo nível são classe da camada de infraestrutura que implementam
		//operações básicas como acesso à dados, ao disco, protocolos de rede, etc.
		//As abstrações seriam as interfaces ou classes abstratas que não possuem implementação.

	//É importante não confundiro princípio da Inversão de dependência com o padrão
	//de projeto da Injeção de dependência.

	//Exemplo Dependency Inversion

	class RecuperarSenha//módulo de alto nível
	{
		private MySqlConnection conexaoBanco;

		public RecuperarSenha()
		{
			conexaoBanco = new MySqlConnection();//Depende da classe MySqlConnection
			conexaoBanco.Conectar();
			//código para recuperar senha
		}
	}

	class MySqlConnection//Módulo de baixo nível
	{
		public void Conectar()
		{
			Console.WriteLine("Conexão com MySql");
			//código...
		}
	}

	//No exemplo acima há um alto acoplamento, pois a classe RecuperarSenha tem a 
	//responsabilidade de criar uma instância da classe MySqlConnection

	//Para resolver o problema de acoplamento acima, vamos aplicar o princípio 
	//da Inversão de dependência:

	interface IDataBaseConnection
	{
		void Conectar();		
	}

	//Módulo de baixo nível herdando de uma abstração(interface)
	class MySqlConnection : IDataBaseConnection
	{
		public void Conectar()
		{
			Console.WriteLine("Conexão com MySql");
			//código da conexão
		}
	}

	class RecuperarSenha//módulo de alto nível com a injeção da dependência em seu construtor
	{
		private IDataBaseConnection _conexaoBanco;

		public RecuperarSenha(IDataBaseConnection conexao)
		{
			_conexaoBanco = conexao;
		}
		_conexaoBanco.Conectar();
		//código para realizar operações
	}

	//Resumo Dependency Inversion

		//Princípio da Inversão de dependência(DIP) => É um princípio que sugere
		//uma solução para o problema da dependência, mas não diz como implementar ou
		// qual técnica usar.

		//Injeção da dependência(DI) => Padrão de projeto usado para implementar a 
		//inversão da dependência. Permite injetar a implementação concreta
		//de um componente de baixo nível em um componente de alto nível.

		//Inversão de controle(IoC) => É uma forma de aplicar a niversão da dependência
		//permitindo que componentes de alto nível dependam de uma abstração e não
		//de um componente de baixo nível.

		//Contâiner IoC => Container de Injeção de dependência é um framework que permite
		//fazer a injeção de dependência de forma automática nos componentes.

//Padrão Repository

	//Um repository é essencialmente uma coleção de "objetos de domínio" em memória, e,
	//com base nisso o padrão Repository permite desacoplar o modelo de domínio
	//do código de acesso à dados.

	//Ao utilizar o padrão Repository, você pode realizar a persistência e a separação
	//de interesses em seu código de acesso à dados visto que ele encapsula
	//a lógica necessária para persistir os objetos do seu domínio
	//na sua fonte de dados.

	//Em uma implementação padrão, podemos começar definindo uma 
	//interface que atuará como a nossa fachada de acesso aos dados
	//e a seguir definir a implementação na classe concreta.

	//Podemos implementar os seguintes tipos de repositório:
		// - Repositório Genérico
		// - Repositório Específico

	//E podemos realziar uma implementação síncrona ou assícrona(Task, async/await)

	//Padrão Repository - Implementação

		//1 - Criar uma interface ou classe abstrata e definir
		//o contrato com os métodos do repositório.

		//2 - Criar a classe concreta que implementa a interface

		//Repositório Genérico:

		//Genérico
		public interface IRepository<T> where T : class
		{
			void Add();
			void Remove();
			void Get();
			T GetId();
			IEnumerable<T> GetAll();
		}

		//Classe implementando
		public class Repository<T> : IRepository<T> where T : class
		{
			...
		}

		//Repositório Específico:

		//Específico 
		public interface IProductRepository<Produto>
		{
			void Add();
			void Remove();
			void Get();
			Produto GetId();
			IEnumerable<Produto> GetAll();
		}

		//Classe implementando
		public class ProdutoRepository<Produto> : IProductRepository<Produto>
		{
			...
		}

		//Genérico ou específico?

			//Um repositório genérico pode ser usado por qualquer entidade
			//de camada de negócios e com isso economizamos código.

			//Ocorre que cada entidade decamada de domínio pode possuir suas
			//particularidades distintas de outras entidades e isso pode
			//inviabilizar o uso de um repositório genérico

			//Assim para decidir é preciso fazer um análise prévia do modelo
			//de domínio e das particularidades de cada entidade.

		//Benefícios do Padrão Repository 

			//Minimiza  alógica de consultas na sua aplicação evitando
			//consultas esparramadas pelo seu código.

			//Encapsula a lógica das consultas em um repositório.

			//Desacopla a sua aplicação dos frameworks de persistência
			//como o Entity Framwork Core

			//Facilita a realização de testes de unidade em sua aplicação

			//Centraliza a lógica de acesso à dados facilitando manutenção
		

//Padrão MVC 

	//O Padrão MVC fornece uma maneira de separar as Funcionalidades
	//e Responsabilidades envolvidas com a manutenção e apresentação
	//dos dados de uma aplicação usando 3 componentes:

		//M => Model: Representa os dados a serem tratados e não inclui
		//detalhes de implementação

		//V => View: Representa o componente de interface com o usuário(UI)
		//e está vínculado ao Model

		//C => Controller: Fornece uma mecanismo para o usuário interagir
		//com o sistema definindo como a interface do usuário vai
		//reagir a ação do usuário. É responsável por trocar e interpretar
		//mensagens entre a View e o Model.

	//Padrão MVC => Padrões de comunicação
		//Permitidos:
			//-Os usuários podem interagir com uma View
			//-Views podem interagir com Controllers
			//-Controllers podem interagir com Views
			//-Controllers podem se comunicar com outros Controllers
			//-Controller podem se comunicar com o Model

		//Não permitidos:
			//-Os usuários não podem interagir diretamente com o Model
			//-Views não podem interagir diretamente com outras Views
			//-Views não podem interagir diretamente com o Model
			//-Models não podem interagir com outros Models

	//Padrão MVC => Benefícios:

		//A View e o Model são desacoplados ou dissociados. Isso significa
		//que você pode ter muitas Views associadas com um determinado Model

		//A dissociação View-Controller permite que você altere a forma
		//como a aplicação responde à entrada do usuário sem alterar
		//o modo de exibição, permitindo que a interface do usuário(view)
		//seja alterada sem alterar a maneira como o aplicativo responde à
		//entrada do usuário.

		//A separação das responsabilidades permite que diferentes membros
		//da equipe possam seconcetrar em uma parte da aplicação que melhor se 
		//se alinha com suas respectivas habilidades.

		//Como o padrão MVC gerencia múltiplos visualizadores usando o
		//mesmo modelo, é fácil manter, testar e atualizar mais de um sistema.

 
//Padrão CQRS

	//O acrônimo CQRS significa Command Query Responsability Segregation ou, numa tradução livre
	//Segregação de Responsibilidade de Consulta e de Comando, e é utilizado para aplicar
	//modelos diferentes para operações de leitura e gravação. 

	//O CQRS é um padrão de projeto arquitetural para separar os "processos de leitura" e "gravação"
	//da sua aplicação. As alterações de dados são realizados via Commands e a leitura de dados é
	//realizada via Queries.

		//Commands => Representa tudo que altera o estado de uma entidade(insert,delete e update)
		//Queries => Não alteram o estado da entidade(select)

		//A utilização do CQRS é indicada para um cenário onde existe uma ALTA DEMANDA de
		//consumo de dados com operações de leitura e escrita feitas de forma BEM INTENSA.

	//O CQRS separa leituras e gravações em modelos diferentes, usando comandos para atualizar
	//dados e consultas para ler dados.

		//Os Comandos devem ser baseados em tarefas, e não centrados em dados;
		
		//Os Comandos podem ser colocados em uma fila para processamento assíncrono,
		//em vez de serem processados de forma síncrona. 

		//As Consultas nunca modificam o banco de dados. Uma consulta retorna um DTO que não encapsula
		//nenhum conhecimento do domínio.

	//Para obter um maior isolamento, você pode separar fisicamente os dados de leitura dos dados
	//de gravação. Nesse caso, o banco de dados pode usar seu próprio esquema de dados otimizado
	//para consultas.

	//Padrão CQRS - Benefícios

		//Escala independente => O CQRS permite que as cargas de trabalho de leitura e gravação
		//sejam escalonadas independentemente, o que pode resultar em menos conteções de bloqueio.

		//Esquemas de dados otimizados => O lado de leitura pode usar um esquema otimizado para
		//consultas, enquanto o lado de gravação usa um esquema otimizado para atualizações.

		//Segurança => É mais fácil garantir que apenas as entidades de domínio corretas estejam
		//executando gravações nos dados.

		//Separação de responsabilidades => A segregação dos dados de leitura e gravação pode resultar
		//em modelos mais flexíveis e fáceis de manter. A maior parte da lógica de negócios complexa
		//entra no modelo de gravação. O modelo de leitura pode ser relativamente simples.

		//Consultas mais simples => Ao armazenar uma view no banco de dados, o aplicativo pode evitar joins
		//complexos durante a consulta, tornando as consultas mais simples.

	//Padrão CQRS - Implementação na ASP.NET Core

		//- Usa a biblioteca MediatR(usa o padrão Mediator)
		//- E o pacote MediatR.Extensions.Microsoft.DependencyInjection

//Arquitetura Monolítica

	//A arquitetura monolítica é um sistema único, não dividido, que roda em um único processo, ou seja,
	//é uma aplicação na qual diferentes componentes estão ligados a um único 
	//programa dentro de uma única plataforma.

	//Na arquitetura monolítica o núcleo do comportamento da aplicação é executado em seu próprio processo
	//e a aplicação inteira é impantada como uma única unidade.

	//Um aplicativo criado com essa arquitetura pode escalar verticalmente aumentando o poder das
	//máquinas em que a aplicação roda ou horizontalmente, com a adição de instâncias atrás de
	//um Load Balancer.

	//Arquiterura Monolítica => 
		//Vantagens: 
			
			//Mais simples de desenvolver.

			//Simples de fazer o deploy(pacote único).

			//Exige uma equipe menor.

		//Desvantagens:

			//Manutenção se torna cada vez maior de acordo com o tamanho da aplicação,
			//o código será cada vez mais difícil de entender e o desafio de fazer 
			//alterações rápidas e subir para o servidor só cresce.

			//Para cada alteração feita, é necessário realizar um novo deploy de toda a aplicação.

			//Fragilidade em uma linha de código que subiu errada pode quebrar todo o sistema
			//e ele ficar totalmente inoperante.

//Arquitetura em Camadas

	//A arquitetura em camadas visa a criação de Aplicativos Modulares, de forma que cada camada possui
	//uma responsabilidade e onde a camada superior se comunica com a camada inferior e assim por diante,
	//fazendo  com que uma camada seja dependente apenas da camada imediatamente inferior.

	//Podemos assim dividir um sistema em uma, duas, três ou N camadas dependendo do objetivo e da
	//complexidade do sistema.

	//Dependendo do contexto as camadas podem ser lógicas(Layers) ou físicas(Tiers).  

	//Arquitetura em Camadas =>
		//Vantagens:

			//Com a organização do código em camadas, podemos reutilizar a funcionalidade de baixo nível
			//em todo o aplicativo.

			//Com uma arquitetura em camadas os aplicativos podem impor restrições sobre quais camadas 
			//podem se comunicar com outras camadas.

			//Essa arquitetura ajuda a atingir o encapsulamento.

			//Quando uma camada é alterada ou substituída, apenas as camadas que interagem
			//com ela serão afetadas.

			//As camada tornam muito mais fácil substituir uma funcionalidade dentro do projeto.

		//Desvantagens:

			//As dependências em tempo de compilação são executadas de cima para baixo.

			//Assim a camada de negócios(BLL) depende dos detalhes de implementação da camada
			//de acesso à dados.

			//Testar a lógica de negócio nesta arquitetura é difícil pois exige um banco de dados de teste.

		//Para resolver esse problema podemos usar a inversão de dependência em uma arquitetura mais robusta
		//como a Arquitetura Cebola(Onion Architecture)