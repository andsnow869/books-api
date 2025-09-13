import React, {useState} from "react";

const FormBook = (props) =>{
    const [bookImgUrl, setBookImgUrl] = useState("");
    const [bookTitle, setBookTitle] = useState("");
    const [bookNameFirst, setBookNameFirst] = useState("");
    const [bookNameLast, setBookNameLast] = useState("");
    const [bookPrice, setBookPrice] = useState("");
    const [bookCategory, setBookCategory] = useState("");
    const [bookDescription, setBookDescription] = useState("");

    const submit = () => {
        if (bookImgUrl.trim() === "" || bookTitle.trim() === ""
           || bookNameFirst.trim() === "" || bookNameLast.trim() === ""
           || bookPrice.trim() === "" || bookCategory.trim() === ""
           || bookDescription.trim() === "") 
           {
            return;
           }
        props.AddBook(
            bookImgUrl,
            bookTitle,
            bookNameFirst,
            bookNameLast,
            bookPrice,
            bookCategory,
            bookDescription
        );
        setBookImgUrl("");
        setBookTitle("");
        setBookNameFirst("");
        setBookNameLast("");
        setBookPrice("");
        setBookCategory("");
        setBookDescription("");
    }


    return(
        <div>
            <div className="mb-3 px-3">
                 <form>
                    <div className="mb-3">
                        <label className="form-label">Обложка книги:</label>
                        <input className="form-control" type="text" value={bookImgUrl} placeholder="https://picsum.photos/200"
                        onChange = {(e) => {setBookImgUrl(e.target.value)}}
                        />
                    </div>
                    <div className="mb-3">
                        <label className="form-label">Название книги:</label>
                        <input className="form-control" placeholder="Например: Евгений Онегин" type="text" 
                        value={bookTitle}
                        onChange = {(e) => {setBookTitle(e.target.value)}}
                        />
                    </div>
                    <div className="mb-3">
                        <label className="form-label">Автор книги:</label>
                        <div className="d-flex">
                            <input className="form-control me-3" placeholder="Александр" type="text"
                            value={bookNameFirst}
                            onChange = {(e) => {setBookNameFirst(e.target.value)}}
                            />
                            <input className="form-control" placeholder="Пушкин" type="text"
                            value={bookNameLast}
                            onChange = {(e) => {setBookNameLast(e.target.value)}}
                            />
                        </div>
                    </div>
                    <div className="mb-3">
                        <label className="form-label">Стоимость книги:</label>
                        <input className="form-control" placeholder="700" type="number"
                        value={bookPrice}
                        onChange = {(e) => {setBookPrice(e.target.value)}}
                        />
                    </div>
                    <div className="mb-3">
                        <label className="form-label">Жанр книги:</label>
                        <input className="form-control" placeholder="Поэзия, Классика" type="text" 
                        value={bookCategory}
                        onChange = {(e) => {setBookCategory(e.target.value)}}
                        />
                    </div>
                    <div className="mb-3">
                        <label className="form-label">Описание книги:</label>
                        <textarea className="form-control" placeholder="Краткое описание книги" type="text" rows={3}
                        value={bookDescription}
                        onChange = {(e) => {setBookDescription(e.target.value)}}
                        ></textarea>
                    </div>
                    
                 </form>
            </div>
            <div>
                <button 
                  className='m-3 rounded-2'
                  onClick={() => {submit()}}
                  >Добавить книгу
                </button>
          </div>
        </div>
    );
}

export default FormBook;