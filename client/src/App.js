import React, { useState } from 'react';
import './App.css';
import ListBooks from './layout/CardBook/components/ListBooks';
import FormBook from './layout/FormBook/FormBook';

const App = () => {
  const [books, setBooks] = useState(
    [{
      img:"https://picsum.photos/200",
      id: crypto.randomUUID(),
      title: "Евгений Онегин",
      name: "Александр Пушкин",
      price: 750,
      category: "Поэзия, Классическая литература",
      description: "Роман в стихах, повествующий о жизни молодого дворянина..."
    },
    {
      img: "https://picsum.photos/200",
      id: crypto.randomUUID(),
      title: "Капитанская дочка",
      name: "Александр Пушкин",
      price: 500,
      category: "Историческая проза",
      description: "История о любви и долге во время крестьянской войны."
    },
    {
      img: "https://picsum.photos/200",
      id:crypto.randomUUID(),
      title: "Мертвые души",
      name: "Николай Гоголь",
      price: 600,
      category: "Реализм, Сатира",
      description: "Роман о путешествии Чичикова по губерниям России."
    }
  ]
  );

const AddContact = (bookImgUrl, bookTitle, bookNameFirst, bookNameLast, bookPrice, bookCategory, bookDescription) => {
  const item = {
      img: bookImgUrl,
      id: crypto.randomUUID(),
      title: bookTitle,
      name: `${bookNameFirst} ${bookNameLast}`,
      price: bookPrice,
      category: bookCategory,
      description: bookDescription
    };
    setBooks([...books, item]);
    console.log(books);
    
}

  return (
    <div className="container mt-5">
      <div className="card">
        <div>
          <h1 className="card-header text-center">Список книг</h1>
          <ListBooks books = {books} />
          <FormBook AddContact = {AddContact} />
        </div>
      </div>
    </div>
  );
}

export default App;
