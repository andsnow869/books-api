import './App.css';
import ListBooks from './layout/CardBook/components/ListBooks';

const books = [{
      id: crypto.randomUUID(),
      title: "Евгений Онегин",
      name: "Александр Пушкин",
      price: 750,
      category: "Поэзия, Классическая литература",
      description: "Роман в стихах, повествующий о жизни молодого дворянина..."
    },
    {
      id: crypto.randomUUID(),
      title: "Капитанская дочка",
      name: "Александр Пушкин",
      price: 500,
      category: "Историческая проза",
      description: "История о любви и долге во время крестьянской войны."
    },
    {
      id:crypto.randomUUID(),
      title: "Мертвые души",
      name: "Николай Гоголь",
      price: 600,
      category: "Реализм, Сатира",
      description: "Роман о путешествии Чичикова по губерниям России."
    }
  ];

const AddContact = () => {
  const item = {
      id: crypto.randomUUID(),
      title: "Мертвые души 5",
      name: "Николай Гоголь 5",
      price: 650,
      category: "Реализм, Сатира",
      description: "Роман о путешествии Чичикова по губерниям России."
    };
    books.push(item);
    console.log(books);
    
}
const App = () => {
  return (
    <div className="container mt-5">
      <div className="card">
        <div>
          <h1 className="card-header text-center">Список книг</h1>
          <ListBooks books = {books} />
          <div>
            <button 
            className='m-3 rounded-2'
            onClick={() => {AddContact ()}}>Добавить книгу</button>
          </div>
        </div>
      </div>
    </div>
  );
}

export default App;
