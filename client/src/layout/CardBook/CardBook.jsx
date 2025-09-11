import React from "react";

const CardBook = (props) => {
    return(
           <div className="card card-hover ">
             <img src="https://picsum.photos/200" className="card-img-top" />
             <div className="card-body">
               <h5 className="card-title">{props.title}</h5>
               <p className="card-name">Автор: {props.name}</p>
               <p className="card-id">id: {props.id}</p>
               <p className="card-price">Цена: {props.price}</p>
               <p className="card-category">Жанр: {props.category}</p>
               <p className="card-description"> Описание: {props.description}</p>
             </div>
           </div>
    );

}



export default CardBook;