import './App.css';
const App = () => {
  return (
    <div className="container mt-5">
      <div className="card">
        <div>
          <h1 className="card-header text-center">Список книг</h1>
        </div>
        <div className="row g-3 m-3">
          {[1,2,3,4].map((_, id) => (
          <div className="col-12 col-md-6 col-lg-3">
           <div className="card card-hover">
             <img src={`https://picsum.photos/200?random=${id}`} className="card-img-top" />
             <div className="card-body">
               <h5 className="card-title">Евгений Онегин</h5>
               <p className="card-name">Автор: Александр Пушкин</p>
               <p className="card-id">id: 10000001-0000-0000-0000-000000000004</p>
               <p className="card-price">Цена: 750.00</p>
               <p className="card-category">Жанр: Поэзия, Классическая литература</p>
               <p className="card-description"> Описание: Роман в стихах, повествующий о жизни молодого дворянина Евгения Онегина в Петербурге и российской провинции 1820-х годов.</p>
             </div>
           </div>
          </div>
          ))}
        </div>
      </div>
    </div>
  );
}

export default App;
