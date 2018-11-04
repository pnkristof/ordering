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


# POST 127.0.0.1:5000/Security/Login
@app.route("/Security/Login", methods=['POST'])
def login():
    parser = reqparse.RequestParser()
    parser.add_argument("Email")
    parser.add_argument("Password")
    args = parser.parse_args()
    if(args["Email"] == "admin@ordering.com"):
        if(args["Password"] == "admin"):
            Security.currentUser = "admin@ordering.com"
            return jsonify("Signed in"), 200
        else:
            return jsonify("Wrong password"), 400
    else:
        Sql = SqlStrings.SqlSelectUser + "WHERE [Email] = \'" + args["Email"] + "\'"
        print("execute SQL:\n" + Sql)
        cursor = connection.execute(Sql)
        for c in cursor:
            if c[2] == args["Email"]:
                Security.currentUser = args["Email"]
                return jsonify("Signed in"), 200
    return jsonify("User doesn't exist"), 404

# POST 127.0.0.1:5000/Security/Logout
@app.route("/Security/Logout", methods=['POST'])
def logout():
    Security.currentUser = None
    return jsonify("Logged out"), 200


# POST 127.0.0.1:5000/Admin/GetUser
@app.route("/Admin/GetUser", methods=['POST'])
def get_user():
    if(Security.currentUser != "admin@ordering.com"):
        return "Permission denied", 401
    parser = reqparse.RequestParser()
    parser.add_argument("Email")
    args = parser.parse_args()
    Sql = SqlStrings.SqlSelectUser + "WHERE [Email] = \'" + args["Email"] + "\'"
    print("execute SQL:\n" + Sql)
    cursor = connection.execute(Sql)
    for c in cursor:
        user = {
			"Name": c[1],
			"Email": c[2],
            "Role": c[3]
			    }
        return jsonify(user), 200
    return jsonify("User with Email " + args["Email"] + " does not exist!"), 404


# GET 127.0.0.1:5000/Admin/GetUse
@app.route("/Admin/GetUsers", methods=['GET'])
def get_users():
    if(Security.currentUser != "admin@ordering.com"):
        return "Permission denied", 401
    users = []
    print("execute SQL:\n" + SqlStrings.SqlSelectUser)
    cursor = connection.execute(SqlStrings.SqlSelectUser)
    for c in cursor:
        user = {
			"Name": c[1],
			"Email": c[2],
            "Role": c[3]
			    }
        users.append(user)
    return jsonify(users), 200


# POST 127.0.0.1:5000/User/CreateUser
@app.route("/User/CreateUser", methods=['POST'])
def create_user():
    parser = reqparse.RequestParser()
    parser.add_argument("Name")
    parser.add_argument("Email")
    user = parser.parse_args()
    if not re.match(r"[^@]+@[^@]+\.[^@]+", user["Email"]):
        return "Invalid Email!", 400
    Sql = SqlStrings.SqlInsertUser + "(\'" + user["Name"] + "\', \'" + user["Email"] + "\', 1)"
    print("execute SQL:\n" + Sql)
    connection.execute(Sql);
    return jsonify(user), 201

# GET 127.0.0.1:5000/User/GetCurrentUser
@app.route("/User/GetCurrentUser", methods=['GET'])
def get_currentuser():
    if Security.currentUser != None:
        Sql = SqlStrings.SqlSelectUser + "WHERE Email = " + "\'"+ Security.currentUser +"\'"
        print("execute SQL:\n" +Sql)
        cursor = connection.execute(Sql);
        for c in cursor:
            user = {
                "Name": c[1],
                "Email": c[2]
                }
        return jsonify(user), 200
    return jsonify("user not found"), 404
    

# POST 127.0.0.1:5000/Admin/CleanDB
@app.route("/Admin/CleanDB", methods=['POST'])
def clean_db():
    if(Security.currentUser != "admin@ordering.com"):
        return "Permission denied", 401
    print("execute SQL:\n" + SqlStrings.SqlCleanUsers)
    connection.execute(SqlStrings.SqlCleanUsers);
    return jsonify("DB Cleaned"), 200


# POST 127.0.0.1:5000/Order/Catalog
@app.route("/Order/Catalog", methods=['POST'])
def get_catalog():
    parser = reqparse.RequestParser()
    parser.add_argument("Categories", action='append')
    args = parser.parse_args()
    products = []
    categories = []
    SqlCatalog = SqlStrings.SqlGetCatalog + "WHERE Category = \'\'"
    if args["Categories"] != None:
        for c in args["Categories"]:
            SqlCatalog += " or Category = \'"+ c +"\'"
    print("execute SQL:\n" + SqlCatalog)
    cursor = connection.execute(SqlCatalog)
    for c in cursor:
        product ={
            "Id": c[0],
            "Name": c[1],
            "Category": c[2],
            "Price": c[3],
            "ImgUrl": c[4]
            }
        products.append(product)
    return jsonify(products), 200



app.run(debug=True)
