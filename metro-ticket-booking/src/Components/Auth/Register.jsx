import React from "react";
import { useNavigate } from "react-router-dom";
import { useFormik } from "formik";
import * as Yup from "yup";
import axios from "axios";

// Import your local background image, adjust path accordingly
import bgImage from '../assets/Login_BGND.jpg'; // update path if needed

const today = new Date();
const minAge = 15;
const minDob = new Date(today.getFullYear() - minAge, today.getMonth(), today.getDate());

export default function Register() {
  const navigate = useNavigate();

  const formik = useFormik({
    initialValues: {
      name: "",
      phone: "",
      dob: "",
      address: "",
      gender: "",
      email: "",
      password: "",
    },
    validationSchema: Yup.object({
      name: Yup.string().required("Required"),
      phone: Yup.string()
        .matches(/^[0-9]{10}$/, "Phone number must be exactly 10 digits")
        .required("Required"),
      dob: Yup.date()
        .max(minDob, `You must be at least ${minAge} years old`)
        .required("Required"),
      address: Yup.string().required("Required"),
      gender: Yup.string().oneOf(["male", "female", "other"], "Invalid Gender").required("Required"),
      email: Yup.string().email("Invalid email address").required("Required"),
      password: Yup.string()
        .min(8, "Password must be at least 8 characters")
        .matches(/[a-z]/, "At least one lowercase character")
        .matches(/[A-Z]/, "At least one uppercase character")
        .matches(/[0-9]/, "At least one number")
        .matches(/[!@#$%^&*]/, "At least one special character")
        .required("Required"),
    }),
    onSubmit: (values, { setSubmitting, setErrors }) => {
      axios.post("http://localhost:5000/api/auth/register", values)
        .then(response => {
          alert("Registration successful! You can now login.");
          navigate("/login");
        })
        .catch(error => {
          alert("Registration failed. Please try again.");
        })
        .finally(() => setSubmitting(false));
    },
  });

  return (
    <>
      {/* Background with blur */}
      <div
        style={{
          position: "fixed",
          top: 0,
          left: 0,
          width: "100vw",
          height: "100vh",
          backgroundImage: `url(${bgImage})`,
          backgroundSize: "cover",
          backgroundPosition: "center",
          filter: "blur(8px)",
          zIndex: -1,
        }}
      />

      {/* Form container */}
      <div
        style={{
          maxWidth: 500,
          margin: "auto",
          padding: 20,
          paddingRight: 50,
          fontSize: 26,
          border: "1px solid black",
          borderRadius: 15,
          position: "fixed",
          top: "50%",
          left: "50%",
          transform: "translate(-50%, -50%)",
          backgroundColor: "rgba(255, 255, 255, 0.85)", // translucent background
          boxShadow: "0 0 15px rgba(0,0,0,0.3)",
          textAlign: "center",
          zIndex: 1,
        }}
      >
        <h2><u>Register</u></h2>
        <form onSubmit={formik.handleSubmit} style={{ padding: 20, marginTop: 20 }}>
          <label style={{ padding: 10, margin: 20, fontWeight: "bold", fontSize: 18 }}>Name:</label>
          <input
            type="text"
            name="name"
            onChange={formik.handleChange}
            onBlur={formik.handleBlur}
            value={formik.values.name}
          />
          {formik.touched.name && formik.errors.name && <div style={{ color: "red" }}>{formik.errors.name}</div>}
          <br />
          <label style={{ padding: 10, fontWeight: "bold", fontSize: 18 }}>Phone Number:</label>
          <input
            type="text"
            name="phone"
            onChange={formik.handleChange}
            onBlur={formik.handleBlur}
            value={formik.values.phone}
          />
          {formik.touched.phone && formik.errors.phone && <div style={{ color: "red" }}>{formik.errors.phone}</div>}
          <br />
          <label style={{ padding: 10, margin: 20, fontWeight: "bold", fontSize: 18 }}>Date of Birth:</label>
          <input
            type="date"
            name="dob"
            onChange={formik.handleChange}
            onBlur={formik.handleBlur}
            value={formik.values.dob}
          />
          {formik.touched.dob && formik.errors.dob && <div style={{ color: "red" }}>{formik.errors.dob}</div>}
          <br />
          <label style={{ padding: 10, margin: 20, fontWeight: "bold", fontSize: 18 }}>Address:</label>
          <input
            type="text"
            name="address"
            onChange={formik.handleChange}
            onBlur={formik.handleBlur}
            value={formik.values.address}
          />
          {formik.touched.address && formik.errors.address && <div style={{ color: "red" }}>{formik.errors.address}</div>}
          <br />
          <label style={{ padding: 10, margin: 20, fontWeight: "bold", fontSize: 18 }}>Gender:</label>
          <select
            name="gender"
            onChange={formik.handleChange}
            onBlur={formik.handleBlur}
            value={formik.values.gender}
          >
            <option value="" label="Select gender" />
            <option value="male" label="Male" />
            <option value="female" label="Female" />
            <option value="other" label="Other" />
          </select>
          {formik.touched.gender && formik.errors.gender && <div style={{ color: "red" }}>{formik.errors.gender}</div>}
          <br />
          <label style={{ padding: 10, margin: 20, fontWeight: "bold", fontSize: 18 }}>Email:</label>
          <input
            type="email"
            name="email"
            onChange={formik.handleChange}
            onBlur={formik.handleBlur}
            value={formik.values.email}
          />
          {formik.touched.email && formik.errors.email && <div style={{ color: "red" }}>{formik.errors.email}</div>}
          <br />
          <label style={{ padding: 10, margin: 20, fontWeight: "bold", fontSize: 18 }}>Password:</label>
          <input
            type="password"
            name="password"
            onChange={formik.handleChange}
            onBlur={formik.handleBlur}
            value={formik.values.password}
          />
          {formik.touched.password && formik.errors.password && <div style={{ color: "red" }}>{formik.errors.password}</div>}
          <br />
          <button
            type="submit"
            disabled={formik.isSubmitting}
            style={{
              padding: "10px 15px",
              fontSize: 18,
              backgroundColor: "blue",
              color: "white",
              border: "none",
              borderRadius: 5,
              marginLeft: 120,
              cursor: formik.isSubmitting ? "not-allowed" : "pointer",
            }}
          >
            Register
          </button>
        </form>
      </div>
    </>
  );
}
