import React from "react";

const FormBook = (props) =>{
    return(
        <div>
            <div className="mb-3 px-3">
                 <form>
                    <div className="mb-3">
                        <label className="form-label" htmlFor="inputGroupFile01">Обложка книги:</label>
                        <input className="form-control" type="file" id="inputGroupFile01"/>
                    </div>
                    <div className="mb-3">
                        <label className="form-label">Название книги:</label>
                        <input className="form-control" placeholder="Например: Евгений Онегин" type="text" />
                    </div>
                    <div className="mb-3">
                        <label className="form-label">Автор книги:</label>
                        <div className="d-flex">
                            <input className="form-control me-3" placeholder="Александр" type="text" />
                            <input className="form-control" placeholder="Пушкин" type="text" />
                        </div>
                    </div>
                    <div className="mb-3">
                        <label className="form-label">Стоимость книги:</label>
                        <input className="form-control" placeholder="700" type="number" />
                    </div>
                    <div className="mb-3">
                        <label className="form-label">Жанр книги:</label>
                        <input className="form-control" placeholder="Поэзия, Классика" type="text" />
                    </div>
                    <div className="mb-3">
                        <label className="form-label">Описание книги:</label>
                        <textarea className="form-control" placeholder="Краткое описание книги" type="text" rows={3}></textarea>
                    </div>
                    
                 </form>
            </div>
            <div>
                <button 
                  className='m-3 rounded-2'
                  onClick={() => {props.AddContact()}}
                  >Добавить книгу
                </button>
          </div>
        </div>
    );
}

export default FormBook;