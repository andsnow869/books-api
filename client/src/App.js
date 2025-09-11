import './App.css';
import CardBook from './layout/CardBook/CardBook';
const App = () => {
  return (
    <div className="container mt-5">
      <div className="card">
        <div>
          <h1 className="card-header text-center">Список книг</h1>
        </div>
        <div className="row g-3 m-3 justify-content-center ">
          <div className="col-12 col-md-6 col-lg-4 d-flex justify-content-center">
            <CardBook />
          </div>
          <div className="col-12 col-md-6 col-lg-4 d-flex justify-content-center">
            <CardBook />
          </div>
          <div className="col-12 col-md-6 col-lg-4 d-flex justify-content-center">
            <CardBook/>
          </div>
        </div>
      </div>
    </div>
  );
}

export default App;
