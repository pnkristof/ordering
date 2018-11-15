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


# POST /Security/Login
@app.route("/Security/Login", methods=['POST'])
def login():
    parser = reqparse.RequestParser()
    parser.add_argument("Email")
    parser.add_argument("Password")
    args = parser.parse_args()
    Sql = SqlStrings.SqlSelectUser + "WHERE [Email] = \'" + args["Email"] + "\'"
    print("\nexecute SQL:\n" + Sql)
    cursor = connection.execute(Sql)
    if cursor != None:
        for c in cursor:
            user = {
                "Id": c[0],
			    "Name": c[1],
			    "Email": c[2],
                "Role": c[3]
			        }
        Security.CurrentUser = user
        return jsonify(user), 200
    return jsonify("User doesn't exist"), 404

# POST /Security/Logout
@app.route("/Security/Logout", methods=['POST'])
def logout():
    Security.currentUser = None
    return jsonify("Logged out"), 200


# POST /Admin/GetUser
@app.route("/Admin/GetUser", methods=['POST'])
def get_user():
    parser = reqparse.RequestParser()
    parser.add_argument("Email")
    args = parser.parse_args()
    Sql = SqlStrings.SqlSelectUser + "WHERE [Email] = \'" + args["Email"] + "\'"
    print("\nexecute SQL:\n" + Sql)
    cursor = connection.execute(Sql)
    for c in cursor:
        user = {
			"Id": c[0],
			"Name": c[1],
			"Email": c[2],
            "Role": c[3]
			    }
        return jsonify(user), 200
    return jsonify("User with Email " + args["Email"] + " does not exist!"), 404


# GET /Admin/GetUsers
@app.route("/Admin/GetUsers", methods=['GET'])
def get_users():
    users = []
    print("\nexecute SQL:\n" + SqlStrings.SqlSelectUser)
    cursor = connection.execute(SqlStrings.SqlSelectUser)
    for c in cursor:
        user = {
			"Id": c[0],
			"Name": c[1],
			"Email": c[2],
            "Role": c[3]
			    }
        users.append(user)
    return jsonify(users), 200


# POST /User/CreateUser
@app.route("/User/CreateUser", methods=['POST'])
def create_user():
    parser = reqparse.RequestParser()
    parser.add_argument("Name")
    parser.add_argument("Email")
    user = parser.parse_args()
    if not re.match(r"[^@]+@[^@]+\.[^@]+", user["Email"]):
        return "Invalid Email!", 400
    Sql = SqlStrings.SqlInsertUser + "(\'" + user["Name"] + "\', \'" + user["Email"] + "\', 1)"
    print("\nexecute SQL:\n" + Sql)
    connection.execute(Sql)
    return jsonify(user), 201

# POST /User/AddAddress
@app.route("/User/AddAddress", methods=['POST'])
def add_address():
    parser = reqparse.RequestParser()
    parser.add_argument("CustomerId")
    parser.add_argument("DeliverTo")
    parser.add_argument("Phone")
    parser.add_argument("Zip")
    parser.add_argument("City")
    parser.add_argument("TheRest")
    args = parser.parse_args()
    Sql = SqlStrings.SqlAddAddress + "(" + args["CustomerId"] + ", \'" + args["DeliverTo"] + "\', \'" + args["Phone"] + "\', \'" + args["Zip"] + "\', \'" + args["City"] + "\', \'" + args["TheRest"] + "\')"
    print("\nexecute SQL:\n" + Sql)
    connection.execute(Sql)
    return jsonify(args), 201

# POST /User/GetAddresses
@app.route("/User/GetAddresses", methods=['POST'])
def get_addresses():
    addresses = []
    parser = reqparse.RequestParser()
    parser.add_argument("UserId")
    args = parser.parse_args()
    Sql = SqlStrings.SqlGetAddresses + "WHERE [CustomerID] = " + args["UserId"]
    print("\nexecute SQL:\n" + Sql)
    cursor = connection.execute(Sql)
    for c in cursor:
        address = {
            "Id" : c[0],
            "CustomerId": c[1],
            "DeliverTo": c[2],
            "Phone": c[3],
            "Zip": c[4],
            "City": c[5],
            "TheRest": c[6]
            }
        addresses.append(address)
    return jsonify(addresses), 200



# GET /User/GetCurrentUser
#@app.route("/User/GetCurrentUser", methods=['GET'])
#def get_currentuser():
#    if Security.currentUser != None:
#        Sql = SqlStrings.SqlSelectUser + "WHERE Email = " + "\'"+
#        Security.currentUser.Email +"\'"
#        print("execute SQL:\n" +Sql)
#        cursor = connection.execute(Sql);
#        for c in cursor:
#            user = {
#                "Name": c[1],
#                "Email": c[2]
#                }
#        return jsonify(user), 200
#    return jsonify("user not found"), 404
    

# POST /Admin/CleanDB
@app.route("/Admin/CleanDB", methods=['POST'])
def clean_db():
    print("\nexecute SQL:\n" + SqlStrings.SqlCleanUsers)
    connection.execute(SqlStrings.SqlCleanUsers)
    return jsonify("DB Cleaned"), 200


# POST /Order/Catalog
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
            SqlCatalog += " or Category = \'" + c + "\'"
    print("\nexecute SQL:\n" + SqlCatalog)
    cursor = connection.execute(SqlCatalog)
    for c in cursor:
        product = {
            "Id": c[0],
            "Name": c[1],
            "Category": c[2],
            "Price": c[3],
            "ImgUrl": c[4]
            }
        products.append(product)
    return jsonify(products), 200

# POST /Ping
@app.route("/Ping", methods=['POST'])
def ping():
    return jsonify("pong"), 200

app.run(debug=True)
