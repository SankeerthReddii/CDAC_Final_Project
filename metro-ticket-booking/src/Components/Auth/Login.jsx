import React from "react";
import { useNavigate, Link } from "react-router-dom";
import { useFormik } from "formik";
import * as Yup from "yup";
import axios from "axios";
import bgImage from '../assets/Login_BGND.jpg';

export default function Login() {
  const navigate = useNavigate();

  // Formik handles form state and validation
  const formik = useFormik({
    initialValues: { email: "", password: "" },
    validationSchema: Yup.object({
      email: Yup.string().email("Invalid email address").required("Required"),
      password: Yup.string().required("Required"),
    }),
    onSubmit: (values, { setSubmitting, setErrors }) => {
      axios.post("http://localhost:5000/api/auth/login", values)
        .then(response => {
          // On successful login, redirect user
          navigate("/user/dashboard");
        })
        .catch(error => {
          // Show error from backend
          setErrors({ password: "Invalid email or password" });
        })
        .finally(() => setSubmitting(false));
    },
  });

  return (
    <>
      {/* Background container with image and blur - styles written here */}
      <div
        style={{
          position: "fixed",
          top: 0,
          left: 0,
          width: "100vw",
          height: "100vh",
          backgroundImage: `url('https://images.unsplash.com/photo-1508051123996-69f8caf4891e?auto=format&fit=crop&w=1470&q=80')`, // metro-related background image
          backgroundSize: "cover",
          backgroundPosition: "center",
          filter: "blur(8px)",
          zIndex: -1,
        }}
      />

      {/* Login form container */}
      <div
        style={{
          maxWidth: 400,
          margin: "auto",
          padding: 20,
          fontSize: 26,
          border: "1px solid black",
          borderRadius: 5,
          position: "fixed",
          top: "50%",
          left: "50%",
          transform: "translate(-50%, -50%)",
          backgroundColor: "rgba(255, 255, 255, 0.85)", // translucent background to highlight form
          boxShadow: "0 0 15px rgba(0,0,0,0.3)",
          textAlign: "center",
        }}
      >
        <h2><u>Login</u></h2>
        <form onSubmit={formik.handleSubmit}>
          <div style={{ marginBottom: 10, fontWeight: "bold", textAlign: "left" }}>
            <label htmlFor="email">Email : </label>
            <input
              type="email"
              id="email"
              name="email"
              onChange={formik.handleChange}
              value={formik.values.email}
              onBlur={formik.handleBlur}
              style={{ width: "100%", padding: 8, fontSize: 16, marginTop: 4 }}
            />
            {formik.touched.email && formik.errors.email ? (
              <div style={{ color: "red" }}>{formik.errors.email}</div>
            ) : null}
          </div>
          <br />
          <div style={{ marginBottom: 10, fontWeight: "bold", textAlign: "left" }}>
            <label htmlFor="password">Password : </label>
            <input
              type="password"
              id="password"
              name="password"
              onChange={formik.handleChange}
              value={formik.values.password}
              onBlur={formik.handleBlur}
              style={{ width: "100%", padding: 8, fontSize: 16, marginTop: 4 }}
            />
            {formik.touched.password && formik.errors.password ? (
              <div style={{ color: "red" }}>{formik.errors.password}</div>
            ) : null}
          </div>
          <br />
          <button
            type="submit"
            disabled={formik.isSubmitting}
            style={{
              padding: "10px 15px",
              fontSize: 26,
              backgroundColor: "blue",
              color: "white",
              border: "none",
              borderRadius: 5,
              marginLeft: 120,
              cursor: formik.isSubmitting ? "not-allowed" : "pointer",
            }}
          >
            Login
          </button>
        </form>
        <p style={{ marginTop: 20 }}>
          Don't have an account? <Link to="/register">Register</Link>
        </p>
      </div>
    </>
  );
}
