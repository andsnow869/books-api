import React from "react";

const BookDetails = () =>
{
  return(
    <div className="container mt-5">
        <h2 className="mb-5">Информация о книге</h2>
        <div className="mb-3">
            <label className="form-label">Обложка книги:</label>
            <input 
                className="form-control" 
                type="text"
                //value = { } 
                onChange={(e) => { }} 
            />
        </div>
        <div className="mb-3">
            <label className="form-label">Название книги:</label>
            <input 
                className="form-control"
                 type="text" 
                 //value={ }
                 onChange={(e) => { }}
            />
        </div>
        <div className="mb-3">
            <label className="form-label">Автор книги:</label>
            <div className="d-flex">
                <input 
                   className="form-control me-3"
                   type="text"
                   //value={} 
                   onChange={(e) => { }}
                />
                <input 
                   className="form-control"
                   type="text"
                   //value={ }
                   onChange={(e) => { }}
               />
            </div>
        </div>
        <div className="mb-3">
            <label className="form-label">Стоимость книги:</label>
            <input 
                className="form-control" 
                type="number"
                //value={}
                onChange={(e) => {}}
            />
        </div>
        <div className="mb-3">
            <label className="form-label">Жанр книги:</label>
            <input 
               className="form-control"
               type="text"
               //value={}
               onChange={(e) => {}}
            />
        </div>
        <div className="mb-3">
            <label className="form-label">Описание книги:</label>
            <input 
               className="form-control"
               type="text"
               //value={}
               onChange={(e) => {}}
            />
        </div>
        <button
            className="rounded-2 me-3" onClick ={(e) => {}}>
            Обновить
        </button>
        <button
            className="rounded-2 me-3" onClick ={(e) => {}}>
            Удалить
        </button>
        <button
            className="rounded-2 me-3" onClick ={(e) => {}}>
            Назад
        </button>

    </div>
  );
}


export default BookDetails;