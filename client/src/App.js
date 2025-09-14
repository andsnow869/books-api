import axios from 'axios';
import React, { useState, useEffect } from 'react';
import './App.css';
import ListBooks from './layout/CardBook/components/ListBooks';
import FormBook from './layout/FormBook/FormBook';

const baseApiUrl = process.env.REACT_APP_API_URL; //адрес, на котором работает бэкенд (из файла .env)

const App = () => {

  const [books, setBooks] = useState([]);

  const url = `${baseApiUrl}/books`;
  useEffect (() =>{
    axios.get(url).then(
    res => setBooks(res.data.books))
}, []);


const AddBook = (bookImgUrl, bookTitle, bookNameFirst, bookNameLast, bookPrice, bookCategory, bookDescription) => {
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
    
}

const DeleteBook = (id) =>{
  setBooks(books.filter(item=>item.id !== id));
}


  return (
    <div className="container mt-5">
      <div className="card">
        <div>
          <h1 className="card-header text-center">Список книг</h1>
          <ListBooks books = {books} 
                     DeleteBook = {DeleteBook}/>
          <FormBook AddBook = {AddBook} />
        </div>
      </div>
    </div>
  );
}

export default App;
