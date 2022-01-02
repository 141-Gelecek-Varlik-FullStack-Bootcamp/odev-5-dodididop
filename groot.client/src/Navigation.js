import React, { Component } from "react";
import { NavLink } from "react-router-dom";
import { Navbar, Nav } from "react-bootstrap";

export class Navigation extends Component {
    render() {
        return (
            <Nav className="justify-content-center" activeKey="/home">
                <Nav.Item>
                    <Nav.Link href="/home">Anasayfa</Nav.Link>
                </Nav.Item>
                <Nav.Item>
                    <Nav.Link href="/user">Kullanıcı</Nav.Link>
                </Nav.Item>
                <Nav.Item>
                    <Nav.Link href="/product">Ürün</Nav.Link>
                </Nav.Item>
            </Nav>
        );
    }
}
