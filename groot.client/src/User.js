import React, { Component } from "react";
import { Button, Form, Row, Col } from "react-bootstrap";

export class User extends Component {
    render() {
        return (
            <div className="mt-5 d-flex justify-content-left">
                <Form>
                    <Row className="mb-3">
                        <Form.Group as={Col}>
                            <Form.Label>İsim</Form.Label>
                            <Form.Control type="firstname" placeholder="İsim" />
                        </Form.Group>

                        <Form.Group as={Col}>
                            <Form.Label>Soyisim</Form.Label>
                            <Form.Control
                                type="lastname" placeholder="Soyisim"/>
                        </Form.Group>
                    </Row>
                    <Row className="mb-3">
                        <Form.Group as={Col} controlId="formGridEmail">
                            <Form.Label>Email</Form.Label>
                            <Form.Control type="email" placeholder="Email" />
                        </Form.Group>

                        <Form.Group as={Col} controlId="formGridPassword">
                            <Form.Label>Şifre</Form.Label>
                            <Form.Control type="password" placeholder="Şifre" />
                        </Form.Group>
                    </Row>

                    <Button variant="primary" type="submit">
                        Kayıt Ol
                    </Button>
                </Form>
            </div>
        );
    }
}
