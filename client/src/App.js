import './App.css';
import ListBooks from './layout/CardBook/components/ListBooks';

const books = [{
      id: 1,
      title: "Евгений Онегин",
      name: "Александр Пушкин",
      price: 750,
      category: "Поэзия, Классическая литература",
      description: "Роман в стихах, повествующий о жизни молодого дворянина..."
    },
    {
      id: 2,
      title: "Капитанская дочка",
      name: "Александр Пушкин",
      price: 500,
      category: "Историческая проза",
      description: "История о любви и долге во время крестьянской войны."
    },
    {
      id: 3,
      title: "Мертвые души",
      name: "Николай Гоголь",
      price: 600,
      category: "Реализм, Сатира",
      description: "Роман о путешествии Чичикова по губерниям России."
    }
  ];

const App = () => {
  return (
    <div className="container mt-5">
      <div className="card">
        <div>
          <h1 className="card-header text-center">Список книг</h1>
          <ListBooks books = {books} />
        </div>
      </div>
    </div>
  );
}

export default App;
