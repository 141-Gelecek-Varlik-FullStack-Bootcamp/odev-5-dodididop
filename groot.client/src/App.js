import './App.css';

import {Home} from './Home';
import {Product} from './Product';
import {User} from './User';
import {Navigation} from './Navigation';

import {BrowserRouter, Route, Routes} from 'react-router-dom';

function App() {
  return (
    <BrowserRouter>
    <div className="container">
     <h3 className="m-3 d-flex justify-content-center">
       Hoşgeldiniz...
     </h3>

     <Navigation/>

     <Routes>
       <Route path='/home' element={<Home/>} />
       <Route path='/product' element={<Product/>}/>
       <Route path='/user' element={<User/>}/>
     </Routes>
      
     
    </div>
    </BrowserRouter>
  );
}

export default App;
