import React, { Component } from "react";
import { Table } from "react-bootstrap";
import axios from "axios";

export class Product extends Component {
    constructor(props) {
        super(props);
        this.state = {
            posts: [],
        };
    }

    componentDidMount() {
        axios.get("https://localhost:51033/api/product").then((response) => {
            this.setState({
                posts: response.data.Entity,
            });
            console.log(response.data.Entity);
        });
    }

    render() {
        const { posts } = this.state;
        return (
            <div>
                <Table striped bordered hover>
                    <thead>
                        <tr>
                            <th>Ürün İsmi</th>
                            <th>Fiyat</th>
                            <th>Stok</th>
                        </tr>
                    </thead>
                    <tbody>
                        {posts.map((pro) => {
                            return (
                                <tr key={pro.Id}>
                                    <td>{pro.DisplayName}</td>
                                    <td>{pro.Price}</td>
                                    <td>{pro.Stock}</td>
                                </tr>
                            );
                        })}
                    </tbody>
                </Table>
            </div>
        );
    }
}
export default Product;
