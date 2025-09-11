import React from "react";

const CardBook = () => {
    return(
           <div className="card card-hover ">
             <img src="https://picsum.photos/200" className="card-img-top" />
             <div className="card-body">
               <h5 className="card-title">Евгений Онегин</h5>
               <p className="card-name">Автор: Александр Пушкин</p>
               <p className="card-id">id: 10000001-0000-0000-0000-000000000004</p>
               <p className="card-price">Цена: 750.00</p>
               <p className="card-category">Жанр: Поэзия, Классическая литература</p>
               <p className="card-description"> Описание: Роман в стихах, повествующий о жизни молодого дворянина Евгения Онегина в Петербурге и российской провинции 1820-х годов.</p>
             </div>
           </div>
    );

}



export default CardBook;