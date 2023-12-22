using System;
using System.Collections;

namespace MVC_PustokPlus.Helpers;

public class PaginationPages
{
	public int CurrentPage { get; private set; }
	public int ItemsPerPage { get; private set; }
	public int TotalPages { get; private set; }
	public bool HasPrev { get; private set; } = false;
	public bool HasNext { get; private set; } = false;

	public string ParentTag { get; set; }
	public string Controller { get; set; }
	public string Action { get; set; }
	public string Attributes { get; set; }


	public PaginationPages(int currentPage, int itemsPerPage, int totalItems,
		string controller, string action = "Index", string attributes = "&", string parentTag = "pagination")
	{
		this.CurrentPage = currentPage;
		this.ItemsPerPage = itemsPerPage;
		this.Controller = controller;
		this.Action = action;
		this.ParentTag = parentTag;
		this.Attributes = attributes;

		this.TotalPages = totalItems / itemsPerPage;
		if (totalItems > this.TotalPages * itemsPerPage) this.TotalPages++;
		if (currentPage > 1) this.HasPrev = true;
		if (currentPage < this.TotalPages) this.HasNext = true;
	}
}

public class Pagination<T> where T : IEnumerable
{
	public T Items { get; private set; }
	public PaginationPages Pagpag { get; private set; }

	public Pagination(T items, int currentPage, int itemsPerPage, int totalItems,
		string controller, string action = "Index", string attributes = "&", string parentTag = "pagination")
	{
		this.Items = items;
		this.Pagpag = new(currentPage, itemsPerPage, totalItems, controller, action, attributes, parentTag);
	}

	public static string MakeRawAttributes(IEnumerable<string attributes)
    {
		string rawAttributes = "&";

		foreach (var attr in attributes)
		{
			rawAttributes += attr.Key + "=" + attr.Value + "&";
		}

		return rawAttributes;
    }
}
