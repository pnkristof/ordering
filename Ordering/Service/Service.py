from flask import Flask, request, jsonify
from flask_restful import Api, Resource, reqparse
import pyodbc
import re
import SqlStrings
import Security

connection = pyodbc.connect("DSN=SQLServer", autocommit=True)

if(connection):
    print("Connected to database")


app = Flask(__name__)
api = Api(app)


@app.route("/Security/Login", methods=['POST'])
def login():
    parser = reqparse.RequestParser()
    parser.add_argument("Email")
    parser.add_argument("Password")
    args = parser.parse_args()
    if(args["Email"] == "admin@ordering.com"):
        if(args["Password"] == "admin"):
            Security.currentUser = "admin@ordering.com"
        else:
            return "Wrong password"
    else:
        Security.currentUser = args["Email"]
    return "Signed in", 200

@app.route("/User/GetUser", methods=['POST'])
def get_user():
    if(Security.currentUser != "admin@ordering.com"):
        return "Permission denied", 401
    parser = reqparse.RequestParser()
    parser.add_argument("Name")
    args = parser.parse_args()
    cursor = connection.execute(SqlStrings.SqlSelectUser + "WHERE [Name] = \'" + args["Name"] + "\'")
    for c in cursor:
        user = {
			"Name": c[1],
			"Email": c[2],
            "Role": c[3]
			    }
        return jsonify(user), 200
    return "User with Name " + args["Name"] + " does not exist!", 404

@app.route("/User/GetUsers", methods=['GET'])
def get_users():
    if(Security.currentUser != "admin@ordering.com"):
        return "Permission denied", 401
    users = []
    cursor = connection.execute(SqlStrings.SqlSelectUser)
    for c in cursor:
        user = {
			"Name": c[1],
			"Email": c[2],
            "Role": c[3]
			    }
        users.append(user)
    return jsonify(users), 200

@app.route("/User/CreateUser", methods=['POST'])
def create_user():
    parser = reqparse.RequestParser()
    parser.add_argument("Name")
    parser.add_argument("Email")
    args = parser.parse_args()
    if not re.match(r"[^@]+@[^@]+\.[^@]+", args["Email"]):
        return "Invalid Email!", 400
    connection.execute(SqlStrings.SqlInsertUser + "(\'" + args["Name"] + "\', \'" + args["Email"] + "\', 1)");
    return "User successfully registered", 201


@app.route("/Admin/CleanDB", methods=['POST'])
def clean_db():
    if(Security.currentUser != "admin@ordering.com"):
        return "Permission denied", 401
    connection.execute(SqlStrings.SqlCleanUsers);
    return "DB Cleaned", 200

app.run(debug=True)
