import React from "react";
import CardBook from "../CardBook";

const ListBooks = (props) =>{
  const books = props.books;
    return(       
      <div className="row g-3 m-3 justify-content-center">
        {books.map((book) => {
          const [bookNameFirst, bookNameLast] = book.name.split(" ");
      
          return(
            <div className="col-12 col-md-6 col-lg-4" key ={book.id}>
            <CardBook {...book} 
                DeleteBook = {props.DeleteBook}
                bookNameFirst = {bookNameFirst}
                bookNameLast = {bookNameLast}
                />
            </div>
        );
})}
     </div>
  );
};

export default ListBooks;
