import React from "react";
import CardBook from "../CardBook";

const ListBooks = (props) =>{
  const books = props.books;
    return(       
      <div className="row g-3 m-3 justify-content-center">
        {books.map((book) => (
            <div className="col-12 col-md-6 col-lg-4">
            <CardBook {...book} 
                DeleteBook = {props.DeleteBook}/>
            </div>
        ))}
     </div>
  );
};

export default ListBooks;
