import React from "react";

const CardBook = (props) => {
    return(
           <div className="card card-hover  d-flex flex-column h-100"
              onClick={() => {props.DeleteBook(props.id)}}
            >
             <img src="https://dummyimage.com/400x400/000/fff" className="card-img-top" />
             <div className="card-body">
               <h5 className="card-title">{props.title}</h5>
               <p className="card-name">Автор: {props.bookNameFirst} {props.bookNameLast}</p>
               <p className="card-id">id: {props.id}</p>
               <p className="card-price">Цена: {props.price}</p>
               <p className="card-category">Жанр: {Array.isArray(props.category) ? props.category.join(", ") : props.category}</p>
               <p className="card-description"> Описание: {props.description}</p>
             </div>
           </div>
    );

}



export default CardBook;